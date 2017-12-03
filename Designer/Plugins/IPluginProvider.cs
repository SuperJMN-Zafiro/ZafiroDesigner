using System.Collections.Generic;
using System.Threading.Tasks;
using Designer.Plugin;

namespace Designer
{
    public interface IPluginProvider
    {
        Task<IList<IImportPlugin>> GetPlugins();
    }
}