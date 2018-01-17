using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Validation
{
    class TimeInputRule : ValidationRule
    {
        private Regex comapreTo = new Regex(@"^[0-5][0-9]:[0-5][0-9]:[0-9]{3}$");
        public TimeInputRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (comapreTo.Match((string)value).Success) return new ValidationResult(true, null);

            else return new ValidationResult(false, "Time should be of format XX:XX:XXX.");
        }

    }
}
