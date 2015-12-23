using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SelializeSample.Converters
{
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var sParam = parameter as string;
            if (sParam == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var enumType = value.GetType();
            if (!Enum.IsDefined(enumType, value))
            {
                return DependencyProperty.UnsetValue;
            }

            var eValue = Enum.Parse(enumType, sParam);
            return (int)eValue == (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var sParam = parameter as string;
            if (sParam == null)
            {
                return DependencyProperty.UnsetValue;
            }

            return Enum.Parse(targetType, sParam);
        }
    }
}
