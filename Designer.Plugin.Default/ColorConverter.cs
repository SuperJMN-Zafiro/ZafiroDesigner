using System.Reflection;
using Designer.Model;
using ExtendedXmlSerializer.ContentModel.Conversion;

namespace Designer.Plugin.Default
{
    public sealed class ColorConverter : IConverter<Color>
    {
        public static ColorConverter Default { get; } = new ColorConverter();
        ColorConverter() {}

        public bool IsSatisfiedBy(TypeInfo parameter) => typeof(Color).GetTypeInfo()
            .IsAssignableFrom(parameter);

        public Color Parse(string data) => ColorExtensions.Parse(data);
            

        public string Format(Color instance)
        {
            var a = instance.A.ToString("X").PadLeft(2, '0');
            var r = instance.R.ToString("X").PadLeft(2, '0');
            var g = instance.G.ToString("X").PadLeft(2, '0');
            var b = instance.B.ToString("X").PadLeft(2, '0');
            return $"#{a}{r}{g}{b}";
        }
    }
}