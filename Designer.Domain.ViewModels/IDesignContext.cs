using System.Collections.Generic;

namespace Designer.Domain.ViewModels
{
    public interface IDesignContext
    {
        ICollection<Graphic> Nodes { get; set; }
        ICollection<Graphic> Selection { get; set; }
        IDesignCommandsHost DesignCommandsHost { get; set; }
    }
}