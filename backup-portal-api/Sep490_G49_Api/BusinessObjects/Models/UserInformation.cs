using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
    public class UserInformation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; } 
        public string? Code { get; set; }
        public string? FullName { get; set; } 
        public string? Gender { get; set; } 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? MatitalStatus { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }
        public string? AddressOfBirth { get; set; } 
        public string? Address { get; set; } 
        public string? HomeTown { get; set; } 
        public string? Ethnic { get; set; } 
        public string? Religious { get; set; } 
        public string? AcademicLevel { get; set; } 
        public bool IsPartyMember { get; set; }
        public bool IsUnionMember { get; set; }
        public string? BankingNo { get; set; } 
        public string? IdCardNo { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DateOfProvide { get; set; }
        public string? AddressOfProvide { get; set; } 
        public string? PitCode { get; set; } 
        public string? SiCode { get; set; } 
        public string? HiCode { get; set; } 
        public string? PassportNo { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PassportIssuedDate { get; set; }
        public string? PassportIssuedAddress { get; set; } 
        public string? DriverLicenseNo { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DriverLicenseIssueDate { get; set; }
        public string? DriverLicenseIssuePlace { get; set; } 
        public string? Note { get; set; } 
        public string? Status { get; set; }
        public string? TypeOfWork { get; set; }
        public string? HealthyStatus { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        //[JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Avatar { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        public List<IFormFile>? AdditionalFiles { get; set; }
        public virtual ICollection<Family> FamilyInformation { get; set; } = new List<Family>();
        public virtual ICollection<UserFile> UserFiles { get; set; } = new List<UserFile>();

    }
}
