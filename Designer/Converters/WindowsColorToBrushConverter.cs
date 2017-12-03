using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Designer.Converters
{
    public class WindowsColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var c = (Color)value;
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var b = (SolidColorBrush) value;
            return b.Color;
        }
    }
}