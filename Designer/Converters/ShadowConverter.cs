using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Designer.Converters
{
    public class ShadowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var array = (object[]) value;

            if (array.Any(o => o == null))
            {
                return 0D;
            }

            var angle = (double)array[0];
            var distance = (double)array[1];

            var s = parameter as string;
            var rad = 2 * Math.PI * angle;
            if (s == "Y")
            {
                return Math.Sin(rad) * distance * 2;
            }
            else
            {
                return Math.Cos(rad) * distance * 2;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}