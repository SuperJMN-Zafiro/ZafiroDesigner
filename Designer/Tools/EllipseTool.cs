using System.Threading.Tasks;
using Designer.Model;
using Rect = Designer.Model.Rect;

namespace Designer.Tools
{
    public class EllipseTool : Tool
    {
        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new Ellipse

            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Background = Color.FromArgb(255, 255, 0,0),
                BackgroundTint = 1,
                Stroke = Color.FromArgb(255,20,0,0)
            });

            return creationResult;
        }

        public EllipseTool(MainViewModel designer) : base(designer)
        {
        }
    }
}