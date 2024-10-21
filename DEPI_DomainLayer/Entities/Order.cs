using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_DomainLayer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public ApplicationUser User { get; set; }
    }
}
