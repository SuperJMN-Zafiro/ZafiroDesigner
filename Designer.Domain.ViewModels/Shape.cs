using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Shape : Graphic
    {
        private Color background;
        private double backgroundTint;
        private Color stroke;
        private double strokeWidth;

        public Color Background
        {
            get => background;
            set => this.RaiseAndSetIfChanged(ref background, value);
        }

        public double BackgroundTint
        {
            get => backgroundTint;
            set => this.RaiseAndSetIfChanged(ref backgroundTint, value);
        }

        public Color Stroke
        {
            get => stroke;
            set => this.RaiseAndSetIfChanged(ref stroke, value);
        }

        public double StrokeWidth
        {
            get => strokeWidth;
            set => this.RaiseAndSetIfChanged(ref strokeWidth, value);
        }
    }
}