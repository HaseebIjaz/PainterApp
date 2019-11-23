using System;
using System.Globalization;
using System.Windows.Data;
using PainterApp.Enums;

namespace PainterApp.Converters
{
    class BooleanEnumConverter : IValueConverter
    {

        public BooleanEnumConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(parameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return parameter;
            return ShapeType.None;
        }

    }
}
