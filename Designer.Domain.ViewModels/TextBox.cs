using Designer.Domain.Models;
using ReactiveUI;
using Zafiro.Core;

namespace Designer.Domain.ViewModels
{
    public class TextBox : Graphic
    {
        private string text;
        private Color background;

        [Hidden]
        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);
        }

        public Color Background
        {
            get => background;
            set => this.RaiseAndSetIfChanged(ref background, value);
        }
    }
}