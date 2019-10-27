using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Rectangle : RectangularGraphic
    {
        private double cornerRadius;

        public double CornerRadius
        {
            get => cornerRadius;
            set => this.RaiseAndSetIfChanged(ref cornerRadius, value);
        }
    }
}