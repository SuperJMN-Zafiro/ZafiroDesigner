using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Designer.Domain.Models;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Designer.Core
{
    public class ImportExtensionsViewModel : ReactiveObject
    {
        private readonly IExtensionsProvider provider;

        public ImportExtensionsViewModel(IExtensionsProvider provider)
        {
            this.provider = provider;


            ImportedProjects = provider.Extensions
                .ToObservableChangeSet()
                .MergeMany(x => x.Import);

            IsBusy = provider.Extensions
                .ToObservableChangeSet()
                .MergeMany(x => x.Import.IsExecuting);
        }

        public IObservable<bool> IsBusy { get; }

        public IObservable<Project> ImportedProjects { get; }

        public ReadOnlyObservableCollection<ImportViewModel> Extensions => provider.Extensions;
    }
}