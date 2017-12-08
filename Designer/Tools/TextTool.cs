using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Tools
{
    public class TextTool : Tool
    {
        public TextTool(MainViewModel designer) : base(designer)
        {
        }

        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var graphic = new TextBox
            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Text = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}{\\f1\\fnil Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\b\\f0\\fs23 OHOHOH\\b0\\f1\\par\r\n}\r\n",
            };

            return new CreationResult(graphic);
        }
    }
}