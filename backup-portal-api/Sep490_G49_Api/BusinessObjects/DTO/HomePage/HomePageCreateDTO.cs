using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.HomePage
{
    public class HomePageCreateDTO
    {
        [Required(ErrorMessage = "TitleBody is required.")]
        [MaxLength(2000, ErrorMessage = "TitleBody cannot exceed 2000 characters.")]
        public string TitleBody { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]

        public string Title { get; set; }
        [Required(ErrorMessage = "StatusJobPost is required.")]

        public string StatusJobPost { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "PhoneNumber is required")]
        [PhoneNumberValidation(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        public IFormFile? ImageBackground { get; set; }
    }
}
