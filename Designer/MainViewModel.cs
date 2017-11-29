using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Designer.Model;
using Designer.Plugin;
using Designer.Tools;
using ReactiveUI;

namespace Designer
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IEnumerable<IImportPlugin> importPlugins;
        private readonly ObservableAsPropertyHelper<IList<Document>> documentsWrapper;
        private readonly ObservableAsPropertyHelper<bool> hasSelectionWrapper;
        private Document selectedDocument;
        private readonly ObservableAsPropertyHelper<bool> isBusyWrapper;

        public MainViewModel(IEnumerable<IImportPlugin> importPlugins)
        {
            this.importPlugins = importPlugins;
            Tools = new List<Tool>
            {
                new RectangleTool(this),
                new EllipseTool(this),
                new LineTool(this),
                new TextTool(this),
                new ImageTool(this),
            };

            PickFileCommand = ReactiveCommand.CreateFromTask(PickFile);
            var documentsObs = PickFileCommand
                .Where(file => file != null)
                .SelectMany(LoadFromFile)
                .ObserveOnDispatcher();
            
            documentsWrapper = documentsObs.ToProperty(this, m => m.Documents);
            SelectedDocumentObservable = this.WhenAnyValue(model => model.SelectedDocument);
            SelectedDocumentObservable.Subscribe(document => { });

            SelectedGraphicsObservable = this
                .WhenAnyObservable(model => model.SelectedDocumentObservable)
                .SelectMany(document =>
                    document == null ? Observable.Return(new List<Graphic>()) : document.SelectedGraphicsObservable);

            HasSomethingSelectedObservable = SelectedGraphicsObservable .Select(list => list.Any());
            HasMoreThanOneSelectedItemObservable = SelectedGraphicsObservable.Select(x => x.Count > 1);

            hasSelectionWrapper = HasSomethingSelectedObservable.ToProperty(this, m => m.HasSomethingSelected);

            AlignCommands = new Commands(this);

            isBusyWrapper = PickFileCommand.IsExecuting.ToProperty(this, model => model.IsBusy);
        }

        public bool IsBusy => isBusyWrapper.Value;

        public IObservable<IList<Graphic>> SelectedGraphicsObservable { get; }
        public IObservable<Document> SelectedDocumentObservable { get; }

        public IObservable<bool> HasSomethingSelectedObservable  { get;}
        public IObservable<bool> HasMoreThanOneSelectedItemObservable { get; }

        public ReactiveCommand<Unit, IStorageFile> PickFileCommand { get; }

        public IList<Document> Documents => documentsWrapper.Value;

        public ICollection<Tool> Tools { get; }

        public Document SelectedDocument
        {
            get => selectedDocument ?? Documents?.FirstOrDefault();
            set => this.RaiseAndSetIfChanged(ref selectedDocument, value);
        }

        public Commands AlignCommands { get; }

        public bool HasSomethingSelected => hasSelectionWrapper.Value;

        private async Task<IList<Document>> LoadFromFile(IStorageFile file)
        {
            var plugin = importPlugins.FirstOrDefault(importPlugin => file.Name.EndsWith(importPlugin.FileExtension));
            if (plugin == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenStreamForReadAsync())
            {
                return await plugin.Load(stream);                
            }
        }

        private async Task<IStorageFile> PickFile()
        {
            var availableExtensions = importPlugins.Select(p => p.FileExtension).ToList();

            if (!availableExtensions.Any())
            {
                return null;
            }

            var picker = new FileOpenPicker
            {
                CommitButtonText = "Abrir",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            foreach (var ext in availableExtensions)
            {
                picker.FileTypeFilter.Add(ext);
            }

            return await picker.PickSingleFileAsync();
        }
    }
}