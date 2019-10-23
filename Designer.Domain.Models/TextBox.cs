namespace Designer.Domain.Models
{
    public class TextBox : Graphic
    {
        public string Text
        {
            get; set;
        }

        public Color Background
        {
            get; set;
        }

        public double Rotation
        {
            get; set;
        }
    }
}