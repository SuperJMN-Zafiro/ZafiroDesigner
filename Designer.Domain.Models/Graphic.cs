namespace Designer.Domain.Models
{
    public abstract class Graphic
    {
        public double Left { get; set; }

        public double Top { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public Color Background { get; set; }

        public Color Stroke { get; set; }

        public double StrokeThickness { get; set; }
    }
}