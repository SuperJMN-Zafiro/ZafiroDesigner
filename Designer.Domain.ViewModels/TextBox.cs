using ReactiveUI;
using Zafiro.Core;

namespace Designer.Domain.ViewModels
{
    public class TextBox : RectangularGraphic
    {
        private string text;

        [Hidden]
        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);
        }
    }
}