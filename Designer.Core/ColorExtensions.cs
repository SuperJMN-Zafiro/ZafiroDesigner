using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Designer.Domain.Models;

namespace Designer.Core
{
    public class ColorExtensions
    {
        public static Color Parse(string str)
        {
            if (str[0] == '#')
            {
                var or = 0u;

                if (str.Length == 7)
                {
                    or = 0xff000000;
                }
                else if (str.Length != 9)
                {
                    throw new FormatException($"Invalid color string: '{str}'.");
                }

                return FromUInt32(uint.Parse(str.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture) | or);
            }
            else
            {
                var upper = str.ToUpperInvariant();
                var member = typeof(Colors).GetTypeInfo().DeclaredProperties
                    .FirstOrDefault(x => x.Name.ToUpperInvariant() == upper);

                if (member != null)
                {
                    return (Color)member.GetValue(null);
                }
                else
                {
                    throw new FormatException($"Invalid color string: '{str}'.");
                }
            }
        }

        public static Color FromUInt32(uint value)
        {
            return Color.FromArgb(
                (byte)((value >> 24) & 0xff),
                (byte)((value >> 16) & 0xff),
                (byte)((value >> 8) & 0xff),
                (byte)(value & 0xff)
            );
        }
    }
}