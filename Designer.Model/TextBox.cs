using ReactiveUI;

namespace Designer.Model
{
    public class TextBox : Graphic
    {
        private string text;
        private Color background;
        private double rotation;

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

        public double Rotation
        {
            get => rotation;
            set => this.RaiseAndSetIfChanged(ref rotation, value);
        }
    }
}