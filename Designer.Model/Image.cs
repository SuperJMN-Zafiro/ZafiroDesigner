using System;
using Zafiro.Core;

namespace Designer.Model
{
    public class Image : Graphic
    {
        [Hidden]
        public byte[] Source { get; set; }
    }
}