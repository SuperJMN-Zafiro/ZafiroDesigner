using System.IO;
using System.Threading.Tasks;
using Designer.Domain.Models;

namespace Designer.Core
{
    public interface IProjectStore
    {
        Task Save(Project project, Stream stream);
        Task<Project> Load(Stream stream);
    }
}