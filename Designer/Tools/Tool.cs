using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Designer.Model;
using ReactiveUI;

namespace Designer.Tools
{
    public abstract class Tool
    {
        protected Tool(MainViewModel designer)
        {
            CreateCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var creationResult = await Create(CreationArea);
                if (creationResult.IsSuccessful)
                {
                    designer.SelectedDocument.Graphics.Add(creationResult.Graphic);
                }
            }, designer.IsDocumentSelectedObservable);
        }

        public Rect CreationArea { get; } = new Rect(0, 0, 200, 100);

        public ICommand CreateCommand { get; }        

        protected abstract Task<CreationResult> Create(Rect creationArea);
    }
}