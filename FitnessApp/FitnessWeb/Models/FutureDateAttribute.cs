using System.ComponentModel.DataAnnotations;

namespace FitnessWeb.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date < DateTime.Now.Date)
                {
                    return new ValidationResult("Date must be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}