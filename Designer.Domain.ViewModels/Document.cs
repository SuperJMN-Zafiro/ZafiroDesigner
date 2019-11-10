using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public class Document : ReactiveObject
    {
        public IEnumerable<Tool> Tools { get; }
        private IEnumerable selectedItems;
        private string name;

        public Document(IEnumerable<Tool> tools)
        {
            Tools = tools;
            Selection = this
                .WhenAnyValue(x => x.SelectedItems)
                .Select(list => list == null ? new List<Graphic>() : list.Cast<Graphic>().ToList());

            AlignChanged = this.WhenAnyValue(x => x.Align);

            Align = ReactiveCommand.Create(() => { });
        }

        public IObservable<ReactiveCommand<Unit, Unit>> AlignChanged { get; set; }

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

        public ReactiveCommand<Unit, Unit> Align { get; set; }
    }
}