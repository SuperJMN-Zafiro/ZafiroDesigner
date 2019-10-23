using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;
using Ellipse = Designer.Domain.ViewModels.Ellipse;
using Rect = Designer.Domain.Models.Rect;

namespace Designer.Core.Tools
{
    public class EllipseTool : Tool
    {
        public EllipseTool(IDesignContext context) : base(context)
        {
        }

        protected override Task<CreationResult> Create(Rect creationArea)
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

            return Task.FromResult(creationResult);
        }
    }
}