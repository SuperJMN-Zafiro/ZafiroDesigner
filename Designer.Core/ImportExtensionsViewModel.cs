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

            Import = ReactiveCommand.CreateFromObservable(() => provider.Extensions
                .ToObservableChangeSet()
                .MergeMany(x => x.Import));

            ImportedProjects = provider.Extensions
                .ToObservableChangeSet()
                .MergeMany(x => x.Import);
        }

        public IObservable<Project> ImportedProjects { get; }

        public ReactiveCommand<Unit, Project> Import { get; }

        public ReadOnlyObservableCollection<ImportExtensionViewModel> Extensions => provider.Extensions;
    }
}