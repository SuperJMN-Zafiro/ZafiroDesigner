using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Designer.Model;
using Designer.Tools;
using ReactiveUI;

namespace Designer
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<IList<Document>> documentsWrapper;
        private readonly ObservableAsPropertyHelper<bool> hasSelectionWrapper;
        private readonly ObservableAsPropertyHelper<bool> isBusyWrapper;
        private readonly IPluginProvider pluginProvider;
        private Document selectedDocument;

        public MainViewModel(IPluginProvider pluginProvider)
        {
            this.pluginProvider = pluginProvider;

            var loadExtensions = new[] {".suppa"};
            var saveExtensions = new[] {new KeyValuePair<string, IList<string>>("Suppa", new List<string> {".suppa"})};
            var fileCommands = new FileCommands<IList<Document>>(() => new List<Document> {new Document()}, LoadFile,
                SaveFile, loadExtensions, saveExtensions);

            documentsWrapper = fileCommands.Objects.ToProperty(this, model => model.Documents);

            SelectedDocumentObservable = this.WhenAnyValue(model => model.SelectedDocument)
                .Where(document => document != null);

            SelectedGraphicsObservable = this
                .WhenAnyObservable(model => model.SelectedDocumentObservable)
                .SelectMany(document => document.SelectedGraphicsObservable);

            HasSomethingSelectedObservable = SelectedGraphicsObservable.Select(list => list.Any());
            HasMoreThanOneSelectedItemObservable = SelectedGraphicsObservable.Select(x => x.Count > 1);

            hasSelectionWrapper = HasSomethingSelectedObservable.ToProperty(this, m => m.HasSomethingSelected);

            isBusyWrapper = fileCommands.IsBusy.ToProperty(this, x => x.IsBusy);

            IsDocumentSelectedObservable = SelectedDocumentObservable.Select(document => document != null);

            AlignCommands = new AlignCommands(this);
            ZOrderCommands = new ZOrderCommands(this);
            FileCommands = fileCommands;

            Tools = new List<Tool>
            {
                new RectangleTool(this),
                new EllipseTool(this),
                new LineTool(this),
                new TextTool(this),
                new ImageTool(this)
            };
        }

        public FileCommands<IList<Document>> FileCommands { get; }

        public ZOrderCommands ZOrderCommands { get; }

        public bool IsBusy => isBusyWrapper.Value;

        public IObservable<IList<Graphic>> SelectedGraphicsObservable { get; }
        public IObservable<Document> SelectedDocumentObservable { get; }

        public IObservable<bool> HasSomethingSelectedObservable { get; }
        public IObservable<bool> HasMoreThanOneSelectedItemObservable { get; }

        public IList<Document> Documents => documentsWrapper.Value;

        public ICollection<Tool> Tools { get; }

        public Document SelectedDocument
        {
            get => selectedDocument ?? Documents?.FirstOrDefault();
            set => this.RaiseAndSetIfChanged(ref selectedDocument, value);
        }

        public AlignCommands AlignCommands { get; }

        public bool HasSomethingSelected => hasSelectionWrapper.Value;

        public IObservable<bool> IsDocumentSelectedObservable { get; }

        private async Task SaveFile(IStorageFile file)
        {
            var plugin =
                (await pluginProvider.GetPlugins()).FirstOrDefault(importPlugin =>
                    file.Name.EndsWith(importPlugin.FileExtension));
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

        private async Task<IList<Document>> LoadFile(IStorageFile file)
        {
            var plugin =
                (await pluginProvider.GetPlugins()).FirstOrDefault(importPlugin =>
                    file.Name.EndsWith(importPlugin.FileExtension));
            if (plugin == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenStreamForReadAsync())
            {
                return await plugin.Load(stream);
            }
        }
    }
}