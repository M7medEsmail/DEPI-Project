using AutoMapper;
using DEPI_BusinessLogicLayer.IRepository;
using DEPI_BusinessLogicLayer.Repository;
using DEPI_DomainLayer.Entities;
using DEPI_GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DEPI_GraduationProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IProductRepository productRepository ,
            IGenericRepository<ProductBrand>BrandRepository,
            IGenericRepository<ProductType> TypeRepository,
            IGenericRepository<Product> repository,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _productRepository = productRepository;
            _brandRepository = BrandRepository;
            _typeRepository = TypeRepository;
            _repository = repository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetFilteredProductsAsync(null ,null);
            return View(products);
        }

  
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id); // Assuming the repository has a GetByIdAsync method

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize(Roles ="Admin")]
        public  async Task <IActionResult> Create()
        {
            // Fetch the list of brands and types from the database
            var brands = await _brandRepository.GetAllAsync(); // Replace with your actual DB call
            var types = await _typeRepository.GetAllAsync();   // Replace with your actual DB call

            // Set the brands and types in the ViewBag
            ViewBag.Brands = brands;
            ViewBag.Types = types;

            return View();

        }
        [HttpPost]
        
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                // Get the file
                var file = productViewModel.PictureUrl;

                // Get the wwwroot path
                var wwwRootPath = _hostEnvironment.WebRootPath;

                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Combine the wwwroot path with the folder path to save the image
                var path = Path.Combine(wwwRootPath, "Images/Product", newFileName);

                // Save the image to the path
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
              
                productViewModel.ImagePath = $"/Images/Product/{newFileName}";

                var product = _mapper.Map<Product>(productViewModel);
                product.PictureUrl = productViewModel.ImagePath;
               
                var type = _repository.AddAsync(product);
                    return RedirectToAction("Index");
                
            }
            return View(productViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> Edit(int id)
        {
            // Fetch the list of brands and types from the database
            var brands = await _brandRepository.GetAllAsync(); // Replace with your actual DB call
            var types = await _typeRepository.GetAllAsync();   // Replace with your actual DB call

            // Set the brands and types in the ViewBag
            ViewBag.Brands = brands;
            ViewBag.Types = types;
            var Product = await _repository.GetByIdAsync(id);
            return View(Product);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id ,ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                // Get the file
                var file = productVM.PictureUrl;

                // Get the wwwroot path
                var wwwRootPath = _hostEnvironment.WebRootPath;

                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Combine the wwwroot path with the folder path to save the image
                var path = Path.Combine(wwwRootPath, "Images/Product", newFileName);

                // Save the image to the path
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                productVM.ImagePath = $"/Images/Product/{newFileName}";

                var product = _mapper.Map<Product>(productVM);
                product.Id = id;
                product.PictureUrl = productVM.ImagePath;
                product.ProductTypeId = productVM.ProductTypeId;
                product.ProductBrandId = productVM.ProductBrandId;
                var type =await _repository.UpdateAsync(id, product);
                return RedirectToAction("Index");

            }
            return View(productVM);
            
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return BadRequest();

            var deleted = await _repository.GetByIdAsync(id);

            return View(deleted);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteAction(int id)
        {
            // Check if the id is valid
            if (id <= 0)
                return BadRequest("Invalid ID.");

            // Attempt to delete the product type and capture the result
            var deleted = await _repository.DeleteAsync(id);

            // Redirect to the index action after successful deletion
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductByBrand(int Id)
        {
            var products = await _productRepository.GetFilteredProductsAsync(Id, null);
            return View("Index", products);
        }

        public async Task<IActionResult> ProductByType(int Id)
        {
            var products = await _productRepository.GetFilteredProductsAsync(null, Id);
            return View("Index", products);
        }
    }
}
