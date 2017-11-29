using System.Threading.Tasks;
using System.Windows.Input;
using Designer.Model;
using Microsoft.Practices.Prism.Commands;

namespace Designer.Tools
{
    public abstract class Tool
    {
        protected Tool(MainViewModel designer)
        {
            CreateCommand = new DelegateCommand(async () =>
            {
                var creationResult = await Create(CreationArea);
                if (creationResult.IsSuccessful)
                {
                    designer.SelectedDocument.Graphics.Add(creationResult.Graphic);
                }
            });
        }

        public Rect CreationArea { get; set; } = new Rect(0, 0, 200, 100);

        public ICommand CreateCommand { get; }        

        protected abstract Task<CreationResult> Create(Rect creationArea);
    }
}