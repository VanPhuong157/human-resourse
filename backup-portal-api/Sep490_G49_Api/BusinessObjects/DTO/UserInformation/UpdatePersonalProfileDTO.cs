using BusinessObjects.Mapping;
using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UpdatePersonalProfileDTO
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(100, ErrorMessage = "FullName can't be longer than 100 characters")]
        [MinLength(6, ErrorMessage = "FullName can't be shorter than 6 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "IdCardNo is required")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "IdCardNo must be exactly 12 characters long")]
        [CCCDValidation(ErrorMessage = "Invalid IdCardNo format.")]
        public string IdCardNo { get; set; }


        [Required(ErrorMessage = "PhoneNumber is required")]
        [PhoneNumberValidation(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime DateOfBirth { get; set; }

        public string? Address { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
