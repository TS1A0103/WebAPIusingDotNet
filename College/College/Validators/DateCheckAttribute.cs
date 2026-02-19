using System.ComponentModel.DataAnnotations;

namespace College.Validators
{
    public class DateCheckAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if (date < DateTime.Now)
            {
                return new ValidationResult("The Date Must be greater than or equal to todays date");
            }
            return ValidationResult.Success;
        }

    }
}
