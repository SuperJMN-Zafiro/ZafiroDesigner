using System;
using System.Collections.ObjectModel;
using Designer.Domain.Models;
using DynamicData;
using ReactiveUI;

namespace Designer.Core
{
    public class ImportExtensionsViewModel : ReactiveObject
    {
        private readonly IExtensionsProvider provider;

        public ImportExtensionsViewModel(IExtensionsProvider provider)
        {
            this.provider = provider;
            ImportedProjects = provider.ObservableChangeset
                .MergeMany(x => x.Import);
        }

        public IObservable<Project> ImportedProjects { get; }

        public ReadOnlyObservableCollection<ExtensionViewModel> Extensions => provider.Extensions;

    }
}