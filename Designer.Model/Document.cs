using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;

namespace Designer.Model
{
    public class Document : ReactiveObject
    {
        private IEnumerable selectedItems;
        public IList<Graphic> Graphics { get; set; }
        public string Name { get; set; }

        public Document()
        {
            SelectedGraphicsObservable = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Graphic>() : list.Cast<Graphic>().ToList());

            HasSelectionObservable = this
                .WhenAnyValue(d => d.SelectedGraphicsObservable)
                .Any();
        }

        public IEnumerable SelectedItems
        {
            get => selectedItems;
            set => this.RaiseAndSetIfChanged(ref selectedItems, value);
        }

        public IObservable<IList<Graphic>> SelectedGraphicsObservable { get; }
        public IObservable<bool> HasSelectionObservable { get; }
        public IList<Graphic> SelectedGraphics => SelectedItems.Cast<Graphic>().ToList();
    }
}