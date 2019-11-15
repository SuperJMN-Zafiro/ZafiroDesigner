using Windows.UI.Xaml;
using Designer.Domain.ViewModels;

namespace Designer.Parts
{
    public sealed partial class EditorControl
    {
        public EditorControl()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            ReactiveUI.MessageBus.Current.SendMessage(new CommandsHostChanged(new DesignCommandsHost(DesignerSurface)));
        }
    }
}
