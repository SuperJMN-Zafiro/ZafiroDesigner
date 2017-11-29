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
                Text = "Sample Text",
            };

            return new CreationResult(graphic);
        }
    }
}