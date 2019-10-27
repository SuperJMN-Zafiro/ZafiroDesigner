using System;
using System.Collections.ObjectModel;
using DynamicData;

namespace Designer.Core
{
    public interface IExtensionsProvider
    {
        IObservable<IChangeSet<ImportViewModel, string>> ObservableChangeset { get; }
        ReadOnlyObservableCollection<ImportViewModel> Extensions { get; }
    }
}