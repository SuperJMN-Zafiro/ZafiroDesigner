using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Designer
{
    public class ObjectTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var key = item.GetType().Name;

            var page = (Page)((Frame)Window.Current.Content).Content;
            

            var entry = page.Resources[key];

            return (DataTemplate)entry;
        }
    }

}