using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Designer
{
    public class ChangeSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return -(double) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return -(double) value;
        }
    }
}