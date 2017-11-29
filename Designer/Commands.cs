using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Designer.Model;
using ReactiveUI;

namespace Designer
{
    public class Commands
    {
        public Commands(MainViewModel mainViewModel)
        {
            AlignToTopCommand = ReactiveCommand
                .Create(() => AlignToTop(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasMoreThanOneSelectedItemObservable);
            AlignToLeftCommand = ReactiveCommand
                .Create(() => AlignToLeft(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasMoreThanOneSelectedItemObservable);
            AlignToRightCommand = ReactiveCommand
                .Create(() => AlignToRight(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasMoreThanOneSelectedItemObservable);
            AlignToBottomCommand = ReactiveCommand
                .Create(() => AlignToBottom(mainViewModel.SelectedDocument.SelectedGraphics),
                    mainViewModel.HasMoreThanOneSelectedItemObservable);
        }

        public ReactiveCommand<Unit, Unit> AlignToTopCommand { get; }

        public ReactiveCommand<Unit, Unit> AlignToRightCommand { get; }

        public ReactiveCommand<Unit, Unit> AlignToLeftCommand { get; }

        public ReactiveCommand<Unit, Unit> AlignToBottomCommand { get; }
        
        private void AlignToTop(IList<Graphic> graphics)
        {
            var x = graphics.Min(g => g.Top);
            foreach (var g in graphics)
            {
                g.Top = x;
            }
        }

        private void AlignToLeft(IList<Graphic> graphics)
        {
            var x = graphics.Min(g => g.Left);
            foreach (var g in graphics)
            {
                g.Left = x;
            }
        }

        private void AlignToRight(IList<Graphic> graphics)
        {
            var x = graphics.Max(g => g.Left + g.Width);
            foreach (var g in graphics)
            {
                g.Left = x - g.Width;
            }
        }

        private void AlignToBottom(IList<Graphic> graphics)
        {
            var x = graphics.Max(g => g.Top + g.Height);
            foreach (var g in graphics)
            {
                g.Top = x - g.Height;
            }
        }
    }
}