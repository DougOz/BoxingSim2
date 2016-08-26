using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Weebul.UI.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {

        

        public Brush TrueBrush { get; set; }

        public Brush FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool && (bool)value)
            {
                return TrueBrush;
            }
            return FalseBrush; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == TrueBrush)
            {
                return true; 
            }
            return false; 
        }
    }
}
