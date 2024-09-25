using BusinessObjects.Mapping;
using BusinessObjects.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class CreateUpdateFamilyDTO
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(100, ErrorMessage = "FullName can't be longer than 100 characters")]
        [MinLength(6, ErrorMessage = "FullName can't be shorter than 6 characters")]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Relationship is required")]
        [MaxLength(50, ErrorMessage = "Relationship can't be longer than 50 characters")]
        public string Relationship { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime DateOfBirth { get; set; }

        [MaxLength(100, ErrorMessage = "Job can't be longer than 100 characters")]
        public string Job { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [MaxLength(15, ErrorMessage = "PhoneNumber can't be longer than 15 characters")]
        [PhoneNumberValidation(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
    }
}
