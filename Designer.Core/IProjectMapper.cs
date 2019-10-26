using Designer.Domain.Models;

namespace Designer.Core
{
    public interface IProjectMapper
    {
        Project Map(Domain.ViewModels.Project project);
        Designer.Domain.ViewModels.Project Map(Project project);
    }
}