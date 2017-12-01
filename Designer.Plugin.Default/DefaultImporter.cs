using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Designer.Model;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;

namespace Designer.Plugin.Default
{
    public class DefaultImporter : ImportPlugin
    {
        private readonly IExtendedXmlSerializer serializer;

        public DefaultImporter()
        {
            serializer = new ConfigurationContainer()
                .EnableParameterizedContent()
                .Create();
        }

        public override string FileExtension => ".sdjmn";
        public override Task<IList<Document>> Load(Stream stream)
        {
            var deserialize = serializer.Deserialize<IList<Document>>(stream);
            return Task.FromResult(deserialize);
        }

        public override Task Save(Stream stream, IList<Document> documents)
        {
            serializer.Serialize(stream, documents);
            return Task.CompletedTask;
        }
    }
}
