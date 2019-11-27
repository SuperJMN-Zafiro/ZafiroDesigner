using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Zafiro.Core;
using Zafiro.Core.Files;

namespace Designer.Core
{
    public class FileCommands<T>
    {
        private readonly string[] loadExtensions;
        private readonly IEnumerable<KeyValuePair<string, IList<string>>> saveExtensions;
        public Func<T> OnCreate { get; }
        public Func<Stream, Task<T>> LoadFile { get; }
        public Func<Stream, Task> SaveFile { get; }

        public FileCommands(IFilePicker filePicker, Func<T> onCreate, Func<Stream, Task<T>> loadFile, Func<Stream, Task> saveFile,
            string[] loadExtensions, IEnumerable<KeyValuePair<string, IList<string>>> saveExtensions)
        {
            this.loadExtensions = loadExtensions;
            this.saveExtensions = saveExtensions;
            OnCreate = onCreate;
            LoadFile = loadFile;
            SaveFile = saveFile;

            CreateNewCommand = ReactiveCommand.Create(onCreate);

            IsBusy = CreateFromExistingCommand.IsExecuting.Merge(SaveFileCommand.IsExecuting);

            CreateFromExistingCommand.ThrownExceptions.Subscribe(exception => { });
            SaveFileCommand.ThrownExceptions.Subscribe(exception => { });
           
        }

        public IObservable<bool> IsBusy { get; }

        public ReactiveCommand<Unit, Stream> SaveFileCommand { get; }

        public IObservable<T> Documents { get; }

        public ReactiveCommand<Unit, Stream> CreateFromExistingCommand { get; }


        public ReactiveCommand<Unit, T> CreateNewCommand { get; }

    }
}