using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Validation
{
    /// <summary>
    /// Validation rule for timing inputs
    /// </summary>
    class TimeInputRule : ValidationRule
    {
        /// <summary>
        /// Regex validating if the data represents a time span MM:ss:mmm
        /// </summary>
        private Regex comapreTo = new Regex(@"^[0-5][0-9]:[0-5][0-9].[0-9]{3}$");

        /// <summary>
        /// Validates value as being a MM:ss.mmm timespan
        /// </summary>
        /// <param name="value">String being checked</param>
        /// <param name="cultureInfo">Supplied by WPF</param>
        /// <returns>Validation results against if value matches the time span required</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (comapreTo.Match((string)value).Success) return new ValidationResult(true, null);

            else return new ValidationResult(false, "Time should be of format mm:ss.fff");
        }

    }
}
