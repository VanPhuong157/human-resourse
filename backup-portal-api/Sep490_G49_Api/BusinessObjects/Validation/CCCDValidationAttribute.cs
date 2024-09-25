using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessObjects.Validation
{
    public class CCCDValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var cccdPattern = @"^\d{9}|\d{12}$"; 
            if (Regex.IsMatch(value.ToString(), cccdPattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid CCCD format.");
            }
        }
    }
}
