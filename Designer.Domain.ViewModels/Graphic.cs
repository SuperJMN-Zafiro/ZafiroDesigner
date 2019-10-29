using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public abstract class Graphic : ReactiveObject
    {
        private double left;
        private double top;
        private double width;
        private double height;
        private Color background;
        private Color stroke;
        private double strokeThickness;

        public double Left
        {
            get => left;
            set => this.RaiseAndSetIfChanged(ref left, value);
        }

        public double Top
        {
            get => top;
            set => this.RaiseAndSetIfChanged(ref top, value);
        }

        public double Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        public double Height
        {
            get => height;
            set => this.RaiseAndSetIfChanged(ref height, value);
        }

        public Color Background
        {
            get => background;
            set => this.RaiseAndSetIfChanged(ref background, value);
        }

        public Color Stroke
        {
            get => stroke;
            set => this.RaiseAndSetIfChanged(ref stroke, value);
        }

        public double StrokeThickness
        {
            get => strokeThickness;
            set => this.RaiseAndSetIfChanged(ref strokeThickness, value);
        }
    }
}