using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;

namespace Designer.Core.Tools
{
    public class RectangleTool : Tool
    {
        public RectangleTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var graphic = new Domain.ViewModels.Rectangle
            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Background = Color.FromArgb(255, 255, 255, 255),
                BackgroundTint = 1,
                Stroke = Color.FromArgb(255, 0, 0, 0),
                StrokeThickness = 1,                
            };

            var creationResult = new CreationResult(graphic);
            return Task.FromResult(creationResult);
        }
    }
}