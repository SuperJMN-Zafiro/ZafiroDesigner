using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Document : ReactiveObject
    {
        private IEnumerable selectedItems;
        private string name;

        public Document()
        {
            Selection = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Graphic>() : list.Cast<Graphic>().ToList());
        }

        public ObservableCollection<Graphic> Graphics { get; set; } = new ObservableCollection<Graphic>();

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public IEnumerable SelectedItems
        {
            get => selectedItems;
            set => this.RaiseAndSetIfChanged(ref selectedItems, value);
        }

        public IObservable<IList<Graphic>> Selection { get; }
    }
}