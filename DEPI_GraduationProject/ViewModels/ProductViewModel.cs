using DEPI_DomainLayer.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DEPI_GraduationProject.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage ="Name is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required!")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile PictureUrl { get; set; }
        public string? ImagePath { get; set; } // Path to save in the database
        public int Stock { get; set; }
        [Display(Name ="Brand")]
        public int ProductBrandId { get; set; } // As A forign Key
        [Display(Name = "Type")]
        public int ProductTypeId { get; set; } // As A forign Key
    }
}
