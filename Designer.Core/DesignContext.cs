using System;
using System.Collections.Generic;
using Designer.Domain.ViewModels;
using ReactiveUI;

namespace Designer.Core
{
    public class DesignContext : ReactiveObject, IDesignContext
    {
        private ICollection<Graphic> nodes;
        private ICollection<Graphic> selection;

        public ICollection<Graphic> Nodes
        {
            get => nodes;
            set => this.RaiseAndSetIfChanged(ref nodes, value);
        }

        public ICollection<Graphic> Selection
        {
            get => selection;
            set => this.RaiseAndSetIfChanged(ref selection, value);
        }

        public IObservable<bool> SelectionObs { get; set; }
    }
}