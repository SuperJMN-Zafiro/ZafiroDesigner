using System.Reactive;
using Designer.Domain.ViewModels;
using ReactiveUI;
using Zafiro.Uwp.Designer;

namespace Designer.Parts
{
    public class DesignCommandsHost : IDesignCommandsHost
    {
        public DesignCommandsHost(DesignerSurface designerSurface)
        {
            AlignLeft = designerSurface.AlignToLeftCommand;
        }

        public ReactiveCommand<Unit, Unit> AlignLeft { get; }
    }
}