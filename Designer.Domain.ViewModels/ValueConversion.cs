using System;
using Designer.Domain.Models;

namespace Designer.Domain.ViewModels
{
    public static class ValueConversion
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


        public static Color TintColor(Color color, double tint)
        {
            return new Color(
                color.A,
                ApplyTint(color.R, tint),
                ApplyTint(color.G, tint),
                ApplyTint(color.B, tint)
            );
        }

        public static byte ApplyTint(byte component, double tint)
        {
            var resultingValue = (1.0 - tint) * 255 + tint * component;
            return (byte)resultingValue;
        }

        public static double Negate(double n)
        {
            return -n;
        }
    }
}