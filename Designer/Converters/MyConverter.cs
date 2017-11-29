using System;
using Windows.UI.Xaml.Media;
using Designer.Model;
using Windows.UI.Xaml;

namespace Designer.Converters
{
    public static class MyConverter
    {
        public static double PolarToCartesianX(double angle, double distance)
        {
            var rad = Math.PI * angle / 180;
            return Math.Cos(rad) * distance;
        }

        public static double PolarToCartesianY(double angle, double distance)
        {
            var rad = 2 * Math.PI * angle / 180;
            return Math.Sin(rad) * distance;
        }

        public static Visibility DistanceToVisibility(double distance)
        {
            if (Math.Abs(distance) < 1)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }
        
        public static SolidColorBrush TintColor(Color color, double tint)
        {           
            return new SolidColorBrush(Windows.UI.Color.FromArgb(
                color.A,
                ApplyTint(color.R, tint),
                ApplyTint(color.G, tint),
                ApplyTint(color.B, tint)
            ));
        }

        public static byte ApplyTint(byte component, double tint)
        {
            var resultingValue = (1.0 - tint) * 255 + tint * component;
            return (byte)resultingValue;
        }

        public static CornerRadius DoubleToCornerRadius(double v)
        {
            return new CornerRadius(v);
        }

        public static double Negate(double n)
        {
            return -n;
        }
    }
}