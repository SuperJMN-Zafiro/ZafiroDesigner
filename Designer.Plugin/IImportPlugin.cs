using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Plugin
{
    public interface IImportPlugin
    {
        string FileExtension { get; }
        Task<IList<Document>> Load(Stream stream);
    }
}