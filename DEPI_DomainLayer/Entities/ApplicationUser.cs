using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_DomainLayer.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturalUrl { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
