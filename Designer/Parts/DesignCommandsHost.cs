using System.Reactive;
using Designer.Domain.ViewModels;
using ReactiveUI;
using Zafiro.Uwp.Design;

namespace Designer.Parts
{
    public class DesignCommandsHost : IDesignCommandsHost
    {
        public DesignCommandsHost(DesignerSurface designerSurface)
        {
            AlignLeft = designerSurface.AlignToLeftCommand;
            AlignTop = designerSurface.AlignToTopCommand;
            AlignRight = designerSurface.AlignToRightCommand;
            AlignBottom = designerSurface.AlignToBottomCommand;
        }

        public ReactiveCommand<Unit, Unit> AlignLeft { get; }
        public ReactiveCommand<Unit, Unit> AlignRight { get; }
        public ReactiveCommand<Unit, Unit> AlignTop { get; }
        public ReactiveCommand<Unit, Unit> AlignBottom { get; }
    }
}