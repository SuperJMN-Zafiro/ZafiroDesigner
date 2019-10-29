using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;

namespace Designer.Core.Tools
{
    public class LineTool : Tool
    {
        protected override Task<CreationResult> Create(Rect creationArea)
        {
            var creationResult = new CreationResult(new Domain.ViewModels.Line
            {
                Left = creationArea.Left,
                Width = creationArea.Width,
                Top = creationArea.Top,
                Height = creationArea.Height,
                Stroke = Color.FromArgb(255,20,0,0),
                StrokeThickness = 1,
                X1 = 0,
                X2 = creationArea.Width,
                Y1 = 0,
                Y2 = creationArea.Height,                
            });

            return Task.FromResult(creationResult);
        }

        public LineTool(IDesignContext context) : base(context)
        {
        }
    }
}