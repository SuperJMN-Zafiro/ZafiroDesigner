using Zafiro.Core;

namespace Designer.Domain.ViewModels
{
    public class Image : Graphic
    {
        [Hidden]
        public byte[] Source { get; set; }
    }
}