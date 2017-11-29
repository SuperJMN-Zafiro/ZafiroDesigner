using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Designer.Converters
{
    public class CornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = (double)value;
            return new CornerRadius(v);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}