using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Designer.Model;

namespace Designer.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var c = (Color)value;
            return new SolidColorBrush(Windows.UI.Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}