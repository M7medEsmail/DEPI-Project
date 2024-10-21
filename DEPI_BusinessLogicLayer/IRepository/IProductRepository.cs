using DEPI_DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_BusinessLogicLayer.IRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetFilteredProductsAsync(int? brandId, int? typeId);
        Task<Product> GetByIdAsync(int id);

    }
}
