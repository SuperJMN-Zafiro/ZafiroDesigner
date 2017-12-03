using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Designer
{
    public class FocusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}