using System;
using Windows.UI.Xaml.Data;
using Designer.Model;

namespace Designer.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return default(Color);
            }

            var c = (Color) value;
            return  Windows.UI.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var c = (Windows.UI.Color)value;
            return new Color(c.A, c.R, c.G, c.B);
        }
    }
}