using System.Reactive;
using ReactiveUI;

namespace Designer.Domain.ViewModels
{
    public interface IDesignCommandsHost
    {
        ReactiveCommand<Unit, Unit> AlignLeft { get; }
    }
}