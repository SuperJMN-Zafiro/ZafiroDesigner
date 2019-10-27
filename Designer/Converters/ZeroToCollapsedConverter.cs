using System;
using Windows.UI.Xaml.Data;

namespace Designer.Converters
{
    public class ZeroToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return MyConverter.DistanceToVisibility((double) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}