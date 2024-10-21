using DEPI_BusinessLogicLayer.IRepository;
using DEPI_DomainLayer.Entities;
using DEPI_GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DEPI_GraduationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;

        public HomeController(ILogger<HomeController> logger,
            IProductRepository productRepository,
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> typeRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAllAsync(); // Replace with your actual DB call
            var types = await _typeRepository.GetAllAsync();   // Replace with your actual DB call

            // Set the brands and types in the ViewBag
            ViewBag.Brands = brands;
            ViewBag.Types = types;

            var products = await _productRepository.GetFilteredProductsAsync(null,null);
                return View(products);       
        }
   
        public async Task<IActionResult> ProductByBrand(int Id)
        {
            var products = await _productRepository.GetFilteredProductsAsync(Id, null);
            return View("ProductByType", products);
        }
    
        public async Task<IActionResult> ProductByType(int Id)
        {
            var products = await _productRepository.GetFilteredProductsAsync(null, Id);
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
