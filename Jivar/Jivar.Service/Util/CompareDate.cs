using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Util
{
    public class CompareDate : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareDate(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (currentValue != null && comparisonValue != null && currentValue >= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be earlier than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }
}
