using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Designer.Plugin;

namespace Designer.Plugins
{
    public class PluginProvider : IPluginProvider
    {
        public async Task<IList<IImportPlugin>> GetPlugins()
        {
            var importPlugins = (await GetAssemblyList())
                .SelectMany(a => a.ExportedTypes)
                .Where(t => !t.IsAbstract && t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IImportPlugin)))
                .Select(type => (IImportPlugin)Activator.CreateInstance(type))
                .ToList();

            return importPlugins;
        }

        public static async Task<List<Assembly>> GetAssemblyList()
        {
            List<Assembly> assemblies = new List<Assembly>();

            var files = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFilesAsync();
            if (files == null)
                return assemblies;

            foreach (var file in files.Where(file => file.FileType == ".dll" || file.FileType == ".exe"))
            {
                try
                {
                    assemblies.Add(Assembly.Load(new AssemblyName(file.DisplayName)));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }

            return assemblies;
        }
    }
}