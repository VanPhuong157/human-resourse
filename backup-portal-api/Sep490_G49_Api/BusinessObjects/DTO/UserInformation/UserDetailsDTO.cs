using BusinessObjects.Mapping;
using BusinessObjects.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.UserInformation
{
    public class UserDetailsDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(100, ErrorMessage = "FullName can't be longer than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [PhoneNumberValidation(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        public string? MatitalStatus { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DateOfBirth { get; set; }

        public string? AddressOfBirth { get; set; }
        public string? Address { get; set; }
        public string? HomeTown { get; set; }
        public string? Ethnic { get; set; }
        public string? Religious { get; set; }
        public string? AcademicLevel { get; set; }
        public bool? IsPartyMember { get; set; }
        public bool? IsUnionMember { get; set; }

        [MaxLength(20, ErrorMessage = "BankingNo can't be longer than 20 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "BankingNo must contain only numbers.")]
        public string? BankingNo { get; set; }

        [MaxLength(20, ErrorMessage = "IdCardNo can't be longer than 20 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "IdCardNo must contain only numbers.")]
        [Required(ErrorMessage = "IdCardNo is required")]
        [CCCDValidation(ErrorMessage ="Invalid IdCardNo format.")]
        public string? IdCardNo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DateOfProvide { get; set; }

        public string? AddressOfProvide { get; set; }

        [StringLength(13, MinimumLength = 10, ErrorMessage = "PitCode must be between 10 and 13 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "PitCode must contain only numbers.")]
        public string PitCode { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "HiCode must be 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "HiCode must contain only numbers.")]
        public string HiCode { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "SiCode must be 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "SiCode must contain only numbers.")]
        public string SiCode { get; set; }

        [MaxLength(20, ErrorMessage = "PassportNo can't be longer than 20 characters")]
        public string? PassportNo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? PassportIssuedDate { get; set; }

        public string? PassportIssuedAddress { get; set; }

        [StringLength(14, MinimumLength = 12, ErrorMessage = "DriverLicenseNo must be between 12 and 14 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "DriverLicenseNo must contain only numbers.")]
        public string DriverLicenseNo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime? DriverLicenseIssueDate { get; set; }

        public string? DriverLicenseIssuePlace { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public string? TypeOfWork { get; set; }
        public string? HealthyStatus { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public List<IFormFile>? AdditionalFiles { get; set; }
    }
}
