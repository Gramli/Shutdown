using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Shutdown.Validators
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);
            var regex = new Regex("[^0-9.-]+");

            if (value is null || !(value is null) && string.IsNullOrEmpty(value.ToString()))
            {
                validationResult = new ValidationResult(false, "Field can't be empty, please Enter Numeric Value");
            }
            else if (regex.IsMatch(value.ToString()))
            {
                validationResult = new ValidationResult(false, "Illegal Characters, please Enter Numeric Value!");
            }

            return validationResult;
        }
    }
}
