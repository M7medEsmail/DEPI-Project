using DEPI_BusinessLogicLayer.IRepository;
using DEPI_DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_GraduationProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<OrderItem> _ordrItemRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService ,
            IGenericRepository<Product> productRepository,
            IGenericRepository<OrderItem> ordrItemRepository,
            SignInManager<ApplicationUser>signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _productRepository = productRepository;
            _ordrItemRepository = ordrItemRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //public async Task< IActionResult> Index(int orderId)
        //{

        //    var items = await _orderService.GetCartItems(orderId);
        //    return View(items);
        //}

        public async Task<IActionResult> GetOrdersByUserId()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var userId = user.Id; // Get the user's ID

                var orders = await _orderService.GetOrdersByUserId(userId);
                return View(orders); // Return the orders to the view
            }

            return RedirectToAction("Login", "Account"); // Redirect to login if user is not signed in
        }
        public async Task<IActionResult> AddToCart(int productId, int quantity,string userId, OrderItem orderItem)
        {

            var product = await _productRepository.GetByIdAsync(productId);
            product.Stock -= quantity;
            await _productRepository.UpdateAsync(product.Id, product);
            orderItem.ProductName = product.Name;
            orderItem.Price = product.Price;
            orderItem.ProductId = productId;
            orderItem.Quantity = quantity;

            await _orderService.AddToCart(orderItem , userId);
            return RedirectToAction("GetOrdersByUserId", new { orderId = orderItem.OrderId });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart(string id ,string quantities)
        {
            var orderIds = id.Split(',').Select(int.Parse).ToList();
            var quantitiesDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, int>>(quantities);
            foreach(var item in quantitiesDict)
            {
                var product =await _productRepository.GetByIdAsync(item.Key);
                product.Stock += item.Value;
                await _productRepository.UpdateAsync(item.Key, product);

            } 

            // Now you have a list of all the OrderIds, perform the ClearCart operation
            foreach (var orderId in orderIds)
            {
                await _orderService.ClearCartAsync(orderId);
                // Example: Perform the ClearCart operation for each OrderId
                // cartService.ClearOrder(orderId);
            }

            
     
            return RedirectToAction("GetOrdersByUserId");
        }

    }
}
