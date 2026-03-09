using System.ComponentModel.DataAnnotations;

namespace MunicipalityManagementSystem.Models
{
    // Custom validation attribute to ensure a user meets the minimum age requirement.
    public class AgeValidationAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public AgeValidationAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                // Adjust if birthday hasn't occurred yet this year
                if (dateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult(
                        $"You must be at least {_minimumAge} years old to register",
                        new[] { validationContext.MemberName! });
                }
            }

            return ValidationResult.Success;
        }
    }
}
