using System.Reactive;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public interface IDesignCommandsHost
    {
        ReactiveCommand<Unit, Unit> AlignLeft { get; }
        ReactiveCommand<Unit, Unit> AlignRight { get; }
        ReactiveCommand<Unit, Unit> AlignTop { get; }
        ReactiveCommand<Unit, Unit> AlignBottom { get; }
    }
}