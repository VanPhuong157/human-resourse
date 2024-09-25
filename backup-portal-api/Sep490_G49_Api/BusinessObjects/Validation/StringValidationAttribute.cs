using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Validation
{
    public class StringValidationAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public StringValidationAttribute(int minLength = 0, int maxLength = int.MaxValue)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var input = value.ToString();
            if (input.Length >= _minLength && input.Length <= _maxLength)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The string length must be between {_minLength} and {_maxLength} characters.");
            }
        }
    }
}
