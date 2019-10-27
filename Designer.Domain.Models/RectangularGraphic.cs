namespace Designer.Domain.Models
{
    public class RectangularGraphic : Graphic
    {
        public double BackgroundTint { get; set; }

        public Shadow Shadow { get; set; } = new Shadow();

        public double Angle { get; set; }
    }
}