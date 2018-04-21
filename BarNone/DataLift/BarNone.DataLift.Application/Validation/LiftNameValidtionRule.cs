using BarNone.DataLift.UI.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Validation
{
    class LiftNameValidtionRule : ValidationRule
    {
        private CurrentLiftDataVM currentLiftData = CurrentLiftDataVMSingleton.GetInstance(); 

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            currentLiftData.LiftInformation[0].LiftNameList.Where(l => l == (string)value).ToList();

            if(currentLiftData.LiftInformation[0].LiftNameList.Where(l => l == (string)value).ToList().Count > 0)
            {
                return new ValidationResult(false, "Enter a valid lift name.");
            }

            return new ValidationResult(true, null);
        }
    }
}
