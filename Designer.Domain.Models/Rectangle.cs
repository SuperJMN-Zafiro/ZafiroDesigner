namespace Designer.Domain.Models
{
    public class Rectangle : Shape
    {
        public double CornerRadius
        {
            get; set;
        }

        public Shadow Shadow { get; set; } = new Shadow();

        public double Rotation
        {
            get; set;
        }
    }
}