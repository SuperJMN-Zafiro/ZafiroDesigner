using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;
using ReactiveUI;

namespace Designer.Model
{
    public class Document : ReactiveObject
    {
        private IEnumerable selectedItems;

        public Document()
        {
            SelectedGraphicsObservable = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Graphic>() : list.Cast<Graphic>().ToList());

            HasSelectionObservable = this
                .WhenAnyValue(d => d.SelectedGraphicsObservable)
                .Any();
        }

        public IList<Graphic> Graphics { get; } = new ObservableCollection<Graphic>();
        public string Name { get; set; }

        [XmlIgnore]
        public IEnumerable SelectedItems
        {
            get => selectedItems;
            set => this.RaiseAndSetIfChanged(ref selectedItems, value);
        }

        [XmlIgnore] public IObservable<IList<Graphic>> SelectedGraphicsObservable { get; }

        [XmlIgnore] public IObservable<bool> HasSelectionObservable { get; }

        [XmlIgnore] public IList<Graphic> SelectedGraphics => SelectedItems.Cast<Graphic>().ToList();
    }
}