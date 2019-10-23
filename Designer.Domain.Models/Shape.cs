namespace Designer.Domain.Models
{
    public class Shape : Graphic
    {
        public Color Background { get; set; }

        public double BackgroundTint { get; set; }

        public Color Stroke { get; set; }

        public double StrokeWidth { get; set; }
    }
}