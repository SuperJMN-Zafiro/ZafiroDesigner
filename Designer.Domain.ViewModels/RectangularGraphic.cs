using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class RectangularGraphic : Graphic
    {
        private readonly ObservableAsPropertyHelper<Color> tintedBackground;
        private double backgroundTint;
        private double angle;

        public RectangularGraphic()
        {
            tintedBackground = this.WhenAnyValue(x => x.Background, x => x.BackgroundTint,
                ValueConversion.TintColor).ToProperty(this, x => x.TintedBackground);
        }

        public Color TintedBackground => tintedBackground.Value;

        public double BackgroundTint
        {
            get => backgroundTint;
            set => this.RaiseAndSetIfChanged(ref backgroundTint, value);
        }

        public double Angle
        {
            get => angle;
            set => this.RaiseAndSetIfChanged(ref angle, value);
        }

        public Shadow Shadow { get; set; } = new Shadow();
    }
}