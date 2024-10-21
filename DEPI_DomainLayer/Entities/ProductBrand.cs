using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_DomainLayer.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
        //public virtual ProductType ProductType { get; set; }
       public ICollection<Product> Products { get; set; }
    }
}
