using DEPI_BusinessLogicLayer.IRepository;
using DEPI_DomainLayer.Context;
using DEPI_DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEPI_BusinessLogicLayer.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreDbContext _context;

        // Inject the context and pass it to the base class
        public ProductRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        // Method to get filtered products by BrandId and/or TypeId
        public async Task<List<Product>> GetFilteredProductsAsync(int? brandId, int? typeId)
        {
            // Start with the base query
            var query = _context.Products.AsQueryable();

            // Apply filtering based on brandId if provided
            if (brandId.HasValue)
            {
                query = query.Where(p => p.ProductBrandId == brandId.Value);
            }

            // Apply filtering based on typeId if provided
            if (typeId.HasValue)
            {
                query = query.Where(p => p.ProductTypeId == typeId.Value);
            }

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        // Method to get a product by its Id
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.ProductBrand)  // Include related ProductBrand entity
                                 .Include(p => p.ProductType)   // Include related ProductType entity
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
