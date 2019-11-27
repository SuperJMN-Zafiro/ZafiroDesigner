using System.Reactive.Linq;
using System.Threading.Tasks;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;
using Zafiro.Core;
using Zafiro.Core.Files;
using Zafiro.Core.Mixins;
using Image = Designer.Domain.ViewModels.Image;

namespace Designer.Core.Tools
{
    public class ImageTool : Tool
    {
        private IFilePicker filePicker;

        public ImageTool(IFilePicker filePicker, IDesignContext context) : base(context)
        {
            this.filePicker = filePicker;
        }

        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var file = await filePicker.Pick("Open", new[] {".png", ".jpg", ".bmp"}).FirstAsync();
            if (file == null)
            {
                return new CreationResult { IsSuccessful = false };
            }

            using (var stream = await file.OpenForRead())
            {
                var bytes = await stream.ReadBytes();
                return new CreationResult(new Image()
                {
                    Width = 200,
                    Height = 200,
                    Source = bytes,
                });
            }
        }
    }
}