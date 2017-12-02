using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Tools
{
    public class RectangleTool : Tool
    {
        public RectangleTool(MainViewModel designer) : base(designer)
        {
        }

        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var graphic = new Rectangle
            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Background = Color.FromArgb(255, 255, 255, 255),
                BackgroundTint = 1,
                Stroke = Color.FromArgb(255, 0, 0, 0),
                StrokeWidth = 1,                
            };

            return new CreationResult(graphic); 
        }
    }
}