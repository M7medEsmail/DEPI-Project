    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_DomainLayer.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
      //  public ICollection<ProductBrand> ProductBrands { get; set; }
       public ICollection<Product> Product { get; set; }
    }
}
