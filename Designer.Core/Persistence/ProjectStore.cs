using System.IO;
using System.Threading.Tasks;
using Designer.Domain.Models;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;

namespace Designer.Core.Persistence
{
    public class ProjectStore : IProjectStore
    {
        private readonly IExtendedXmlSerializer serializer;

        public ProjectStore()
        {
            serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableParameterizedContent()
                .Register(ColorConverter.Default)
                .Create();
        }

        public Task<Project> Load(Stream stream)
        {
            var project = serializer.Deserialize<Project>(stream);
            return Task.FromResult(project);
        }

        public Task Save(Project project, Stream stream)
        {
            serializer.Serialize(stream, project);
            return Task.CompletedTask;
        }
    }
}