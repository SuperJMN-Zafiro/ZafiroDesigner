using System;
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

        public void SetAbsolutePoints(Point p1, Point p2)
        {
            var x1 = Math.Min(p1.X, p2.X);
            var x2 = Math.Max(p1.X, p2.X);

            var y1 = Math.Min(p1.Y, p2.Y);
            var y2 = Math.Max(p1.Y, p2.Y);

            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;

            Width = X2 - X1;
            Height = Y2 - Y1;
            Left = X1;
            Top = Y1;
        }
    }
}