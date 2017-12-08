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
        private readonly IPluginProvider pluginProvider;
        private readonly ObservableAsPropertyHelper<IList<Document>> documentsWrapper;
        private readonly ObservableAsPropertyHelper<bool> hasSelectionWrapper;
        private Document selectedDocument;
        private readonly ObservableAsPropertyHelper<bool> isBusyWrapper;

        public MainViewModel(IPluginProvider pluginProvider)
        {
            this.pluginProvider = pluginProvider;
            OpenFileCommand = ReactiveCommand.CreateFromTask(PickFileToOpen);
           
            NewFileCommand = ReactiveCommand.Create(() => new List<Document>() { new Document() });

            var documentsObs = OpenFileCommand
                .Where(file => file != null)
                .SelectMany(LoadFile)
                .Merge(NewFileCommand)
                .ObserveOnDispatcher();

            SaveFileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var pickFileToSave = await PickFileToSave();
                if (pickFileToSave != null)
                {
                    await SaveFile(pickFileToSave);
                }
            }, documentsObs.Any());

            SaveFileCommand.ThrownExceptions.Subscribe(exception => { });


            documentsWrapper = documentsObs.ToProperty(this, m => m.Documents);
            SelectedDocumentObservable = this.WhenAnyValue(model => model.SelectedDocument)
                .Where(document => document != null);

            SelectedGraphicsObservable = this
                .WhenAnyObservable(model => model.SelectedDocumentObservable)
                .SelectMany(document => document.SelectedGraphicsObservable);

            HasSomethingSelectedObservable = SelectedGraphicsObservable.Select(list => list.Any());
            HasMoreThanOneSelectedItemObservable = SelectedGraphicsObservable.Select(x => x.Count > 1);

            hasSelectionWrapper = HasSomethingSelectedObservable.ToProperty(this, m => m.HasSomethingSelected);

            

            isBusyWrapper = OpenFileCommand.IsExecuting.Merge(SaveFileCommand.IsExecuting).ToProperty(this, model => model.IsBusy);

            IsDocumentSelectedObservable = SelectedDocumentObservable.Select(document => document != null);

            AlignCommands = new AlignCommands(this);
            ZOrderCommands = new ZOrderCommands(this);


            Tools = new List<Tool>
            {
                new RectangleTool(this),
                new EllipseTool(this),
                new LineTool(this),
                new TextTool(this),
                new ImageTool(this),
            };
        }

        public ZOrderCommands ZOrderCommands { get; }

        private async Task SaveFile(IStorageFile file)
        {
            var plugin = (await pluginProvider.GetPlugins()).FirstOrDefault(importPlugin => file.Name.EndsWith(importPlugin.FileExtension));
            if (plugin == null)
            {
                throw new InvalidOperationException("No plugins to save this file type!");
            }

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                await plugin.Save(stream, Documents);
                await stream.FlushAsync();
            }
        }

        public ReactiveCommand<Unit, Unit> SaveFileCommand { get; set; }

        private async Task<IStorageFile> PickFileToSave()
        {
            var availableExtensions = (await pluginProvider.GetPlugins()).Select(p => p.FileExtension).ToList();

            if (!availableExtensions.Any())
            {
                return null;
            }

            var picker = new FileSavePicker
            {
                CommitButtonText = "Save",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            foreach (var ext in availableExtensions)
            {
                picker.FileTypeChoices.Add(new KeyValuePair<string, IList<string>>(ext, new List<string>() { ext }));
            }

            return await picker.PickSaveFileAsync();
        }

        public bool IsBusy => isBusyWrapper.Value;

        public IObservable<IList<Graphic>> SelectedGraphicsObservable { get; }
        public IObservable<Document> SelectedDocumentObservable { get; }

        public IObservable<bool> HasSomethingSelectedObservable { get; }
        public IObservable<bool> HasMoreThanOneSelectedItemObservable { get; }

        public ReactiveCommand<Unit, IStorageFile> OpenFileCommand { get; }

        public IList<Document> Documents => documentsWrapper.Value;

        public ICollection<Tool> Tools { get; }

        public Document SelectedDocument
        {
            get => selectedDocument ?? Documents?.FirstOrDefault();
            set => this.RaiseAndSetIfChanged(ref selectedDocument, value);
        }

        public AlignCommands AlignCommands { get; }

        public bool HasSomethingSelected => hasSelectionWrapper.Value;

        public ReactiveCommand<Unit, List<Document>> NewFileCommand { get; }
        public IObservable<bool> IsDocumentSelectedObservable { get; }

        private async Task<IList<Document>> LoadFile(IStorageFile file)
        {
            var plugin = (await pluginProvider.GetPlugins()).FirstOrDefault(importPlugin => file.Name.EndsWith(importPlugin.FileExtension));
            if (plugin == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenStreamForReadAsync())
            {
                return await plugin.Load(stream);
            }
        }

        private async Task<IStorageFile> PickFileToOpen()
        {
            var availableExtensions = (await pluginProvider.GetPlugins()).Select(p => p.FileExtension).ToList();

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