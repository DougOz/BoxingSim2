using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Weebul.Core.Model;

namespace Weebul.UI.Converters
{
    public class PivotDictionaryAverageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Dictionary<PivotFighter, PivotFightResultSet>)
            {
                var v = value as Dictionary<PivotFighter, PivotFightResultSet>;
                return v.Average(a => a.Value.WinPercentage);


            }
            return null; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
