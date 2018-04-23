using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using ReactiveUI;

namespace Designer
{
    public class FileCommands<T>
    {
        private readonly string[] loadExtensions;
        private readonly IEnumerable<KeyValuePair<string, IList<string>>> saveExtensions;
        public Func<T> OnCreate { get; }
        public Func<IStorageFile, Task<T>> LoadFile { get; }
        public Func<IStorageFile, Task> SaveFile { get; }

        public FileCommands(Func<T> onCreate, Func<IStorageFile, Task<T>> loadFile, Func<IStorageFile, Task> saveFile, string[] loadExtensions, IEnumerable<KeyValuePair<string, IList<string>>> saveExtensions)
        {
            this.loadExtensions = loadExtensions;
            this.saveExtensions = saveExtensions;
            OnCreate = onCreate;
            LoadFile = loadFile;
            SaveFile = saveFile;

            CreateNewCommand = ReactiveCommand.Create(onCreate);

            CreateFromExistingCommand = ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(PickFileToOpen)
                .Where(f => f != null)
                .SelectMany(LoadFile));

            Objects = CreateNewCommand.Merge(CreateFromExistingCommand);

            SaveFileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var pickFileToSave = await PickFileToSave();
                if (pickFileToSave != null)
                {
                    await SaveFile(pickFileToSave);
                }
            }, CreateFromExistingCommand.Any());

            IsBusy = CreateFromExistingCommand.IsExecuting.Merge(SaveFileCommand.IsExecuting);

            CreateFromExistingCommand.ThrownExceptions.Subscribe(exception => { });
            SaveFileCommand.ThrownExceptions.Subscribe(exception => { });
        }

        public IObservable<bool> IsBusy { get; }

        public ReactiveCommand<Unit, Unit> SaveFileCommand { get; }

        public IObservable<T> Objects { get; }

        public ReactiveCommand<Unit, T> CreateFromExistingCommand { get; }
        
        private async Task<IStorageFile> PickFileToOpen()
        {            
            var picker = new FileOpenPicker
            {
                CommitButtonText = "Open",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            foreach (var ext in loadExtensions)
            {
                picker.FileTypeFilter.Add(ext);
            }

            return await picker.PickSingleFileAsync();
        }

        
        private async Task<IStorageFile> PickFileToSave()
        {           
            var picker = new FileSavePicker
            {
                CommitButtonText = "Save",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            foreach (var pair in saveExtensions)
            {
                picker.FileTypeChoices.Add(pair);
            }

            return await picker.PickSaveFileAsync();
        }

        public ReactiveCommand<Unit, T> CreateNewCommand { get; }
    }
}