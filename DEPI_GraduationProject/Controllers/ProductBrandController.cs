using AutoMapper;
using DEPI_BusinessLogicLayer.IRepository;
using DEPI_DomainLayer.Entities;
using DEPI_GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_GraduationProject.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IGenericRepository<ProductBrand> _repository;
        private readonly IMapper _mapper;

        public ProductBrandController(IGenericRepository<ProductBrand> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var productTypes = await _repository.GetAllAsync(); // Fetch the list of product types
            return View(productTypes);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrandVM productBrand)
        {
            if (ModelState.IsValid)
            {
               var brand = _mapper.Map<ProductBrand>(productBrand);
                _repository.AddAsync(brand);
                return RedirectToAction("Index");
            }
            return View(productBrand);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductBrandVM productBrand)
        {

            var EditBarnd = _mapper.Map<ProductBrand>(productBrand);
            EditBarnd.Id = id;
            var type = await _repository.UpdateAsync(id, EditBarnd);
            return RedirectToAction("Index");
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



    }
}

