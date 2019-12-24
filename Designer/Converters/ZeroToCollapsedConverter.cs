using System;
using Windows.UI.Xaml.Data;

namespace Designer.Converters
{
    public class ZeroToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Conversion.DistanceToVisibility((double) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}