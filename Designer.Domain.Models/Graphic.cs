namespace Designer.Domain.Models
{
    public abstract class Graphic
    {
        public double Left { get; set; }

        public double Top { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Angle { get; set; }
    }
}