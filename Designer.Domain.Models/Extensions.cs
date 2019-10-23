namespace Designer.Domain.Models
{
    public static class Extensions
    {
        public static Rect GetBounds(this Graphic g)
        {
            return new Rect(g.Left, g.Top, g.Width, g.Height);
        }
    }
}