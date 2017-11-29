using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Designer.Model;

namespace Designer.Converters
{
    public class TintedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var objects = (object[]) value;

            if (objects.Any(o => o == null))
            {
                return null;
            }

            var color = (Color)objects.Single(o => o is Color);
            var tint = (double)objects.Single(o => o is double);


            return new SolidColorBrush(Windows.UI.Color.FromArgb(
                color.A,
                ApplyTint(color.R, tint),
                ApplyTint(color.G, tint),
                ApplyTint(color.B, tint)
                ));
        }

        private byte ApplyTint(byte component, double tint)
        {
            var resultingValue = (1.0 - tint) * 255 + tint * component;
            return (byte)resultingValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}