using System;
using System.Collections.ObjectModel;
using DynamicData;

namespace Designer.Core
{
    public interface IExtensionsProvider
    {
        IObservable<IChangeSet<ImportExtensionViewModel, string>> ObservableChangeset { get; }
        ReadOnlyObservableCollection<ImportExtensionViewModel> Extensions { get; }
    }
}