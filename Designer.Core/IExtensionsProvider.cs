using System;
using System.Collections.ObjectModel;
using DynamicData;

namespace Designer.Core
{
    public interface IExtensionsProvider
    {
        IObservable<IChangeSet<ExtensionViewModel, string>> ObservableChangeset { get; }
        ReadOnlyObservableCollection<ExtensionViewModel> Extensions { get; }
    }
}