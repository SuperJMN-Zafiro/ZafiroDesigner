using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Line : Graphic
    {
        private double x1;
        private double x2;
        private double y1;
        private double y2;

        public double X1
        {
            get { return x1; }
            set { this.RaiseAndSetIfChanged(ref x1, value); }
        }

        public double X2
        {
            get { return x2; }
            set { this.RaiseAndSetIfChanged(ref x2, value); }
        }

        public double Y1
        {
            get { return y1; }
            set { this.RaiseAndSetIfChanged(ref y1, value); }
        }

        public double Y2
        {
            get { return y2; }
            set { this.RaiseAndSetIfChanged(ref y2, value); }
        }

        public Point StartPoint => new Point(X1, Y1);
        public Point EndPoint => new Point(X2, Y2);
    }
}