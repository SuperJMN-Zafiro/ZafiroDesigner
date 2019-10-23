namespace Designer.Domain.Models
{
    public class Shadow
    {
        private double distance;
        private double angle;
        private Color color;

        public double Distance
        {
            get; set;
        }

        public double Angle
        {
            get; set;
        }

        public Color Color
        {
            get; set;
        }

        protected bool Equals(Shadow other)
        {
            return distance.Equals(other.distance) && angle.Equals(other.angle) && color.Equals(other.color);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Shadow) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = distance.GetHashCode();
                hashCode = (hashCode * 397) ^ angle.GetHashCode();
                hashCode = (hashCode * 397) ^ color.GetHashCode();
                return hashCode;
            }
        }
    }
}