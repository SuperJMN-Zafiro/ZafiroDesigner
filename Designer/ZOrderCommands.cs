using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Designer.Model;
using ReactiveUI;

namespace Designer
{
    public class ZOrderCommands
    {
        private readonly MainViewModel mainViewModel;

        public ZOrderCommands(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SendToFrontCommand = ReactiveCommand
                .Create(() => SendToFront(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasSomethingSelectedObservable);
            SendToBackCommand = ReactiveCommand
                .Create(() => SendToBack(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasSomethingSelectedObservable);
        }

        private void SendToFront(IList<Graphic> selectedItems)
        {
            foreach (var s in selectedItems)
            {
                mainViewModel.SelectedDocument.Graphics.Remove(s);
            }

            foreach (var selectedItem in selectedItems)
            {
                mainViewModel.SelectedDocument.Graphics.Add(selectedItem);
            }
        }

        private void SendToBack(IList<Graphic> selectedItems)
        {
            foreach (var s in selectedItems)
            {
                mainViewModel.SelectedDocument.Graphics.Remove(s);
            }

            foreach (var selectedItem in selectedItems)
            {
                mainViewModel.SelectedDocument.Graphics.Insert(0, selectedItem);
            }
        }

        public ReactiveCommand<Unit, Unit> SendToBackCommand { get; set; }

        public ReactiveCommand<Unit, Unit> SendToFrontCommand { get; set; }
    }
}