using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Designer.Model;
using Microsoft.Toolkit.Uwp.Helpers;

namespace Designer.Tools
{
    public class ImageTool : Tool
    {
        protected override async Task<CreationResult> Create(Rect creationArea)
        {
            var picker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
            picker.FileTypeFilter.Add(".jpg");
            picker.CommitButtonText = "Load";

            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                return new CreationResult();
            }

            var bytes = await file.ReadBytesAsync();
            
            return new CreationResult(new Image()
            {
                Width = 200,
                Height = 200,
                Source = bytes,
            });
        }

        public ImageTool(MainViewModel designer) : base(designer)
        {
        }
    }
}