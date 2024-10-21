using System.ComponentModel.DataAnnotations;

namespace DEPI_GraduationProject.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile PictureUrl { get; set; }
        public string? ImagePath { get; set; } 
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
