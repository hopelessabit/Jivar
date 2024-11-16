using System.ComponentModel.DataAnnotations;

namespace Jivar.Service.Util
{
    public class CompareDateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareDateAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue == null || comparisonValue == null)
                return ValidationResult.Success;

            // Kiểm tra nếu ngày của StartDate lớn hơn ngày của EndDate
            if (currentValue.Value.Date > comparisonValue.Value.Date)
            {
                return new ValidationResult(ErrorMessage ?? "Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
            }
            // Nếu cùng ngày, kiểm tra thời gian
            else if (currentValue.Value.Date == comparisonValue.Value.Date &&
                     currentValue.Value.TimeOfDay > comparisonValue.Value.TimeOfDay)
            {
                return new ValidationResult(ErrorMessage ?? "Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc trong cùng ngày");
            }
            return ValidationResult.Success;
        }
    }
}
