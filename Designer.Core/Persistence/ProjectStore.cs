using System.IO;
using System.Threading.Tasks;
using Designer.Domain.Models;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;

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
                .Type<Color>().Register().Converter().Using(ColorConverter.Default)
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