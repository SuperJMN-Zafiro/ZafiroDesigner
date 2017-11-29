using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Designer.Model;

namespace Designer.Plugin
{
    public abstract class ImportPlugin : IImportPlugin
    {
        public abstract string FileExtension { get; }
        public abstract Task<IList<Document>> Load(Stream stream);
    }    
}