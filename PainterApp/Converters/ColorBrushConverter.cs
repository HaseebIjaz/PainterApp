using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PainterApp.Converters
{
    class ColorBrushConverter : IValueConverter
    {
        public ColorBrushConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value"); //mentioning the name of the parameter that causes exception
            }

            var brush = (SolidColorBrush)value;

            if (brush == null)
            {
                throw new NullReferenceException("Null reference due to Advanced Color selection which is not expected input for SolidColorBrush");
            }
            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            Color color = (value as Color?).Value;
            return new SolidColorBrush(color);
        }
    }
}
