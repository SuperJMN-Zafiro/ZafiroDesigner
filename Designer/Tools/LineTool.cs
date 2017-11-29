using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Tools
{
    public class LineTool : Tool
    {
        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new Line
            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Background = Color.FromArgb(255, 255, 0,0),
                BackgroundTint = 1,
                Stroke = Color.FromArgb(255,20,0,0),
                StrokeWidth = 1,
                X1 = 0,
                X2 = creationArea.Width,
                Y1 = 0,
                Y2 = creationArea.Height,                
            });

            return creationResult;
        }

        public LineTool(MainViewModel designer) : base(designer)
        {
        }
    }
}