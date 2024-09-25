using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessObjects.Validation
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var phonePattern = @"^(0|\+84)(3[2-9]|5[2689]|7[06-9]|8[1-9]|9[0-9])[0-9]{7}$";

            if (Regex.IsMatch(value.ToString(), phonePattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid phone number format.");
            }
        }
    }
}
