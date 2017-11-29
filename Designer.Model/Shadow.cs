using ReactiveUI;

namespace Designer.Model
{
    public class Shadow : ReactiveObject
    {
        private double distance;
        private double angle;
        private Color color;

        public double Distance
        {
            get => distance;
            set => this.RaiseAndSetIfChanged(ref distance, value);
        }

        public double Angle
        {
            get => angle;
            set => this.RaiseAndSetIfChanged(ref angle, value);
        }

        public Color Color
        {
            get => color;
            set => this.RaiseAndSetIfChanged(ref color, value);
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