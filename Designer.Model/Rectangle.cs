using ReactiveUI;

namespace Designer.Model
{
    public class Rectangle : Shape
    {
        private double cornerRadius;
        private double rotation;

        public double CornerRadius
        {
            get => cornerRadius;
            set => this.RaiseAndSetIfChanged(ref cornerRadius, value);
        }

        public Shadow Shadow { get; set; } = new Shadow()
        {
            Angle = 45,
            Distance = 10,
        };

        public double Rotation
        {
            get => rotation;
            set => this.RaiseAndSetIfChanged(ref rotation, value);
        }
    }
}