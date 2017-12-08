using System;
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
                .UseAutoFormatting()
                .UseOptimizedNamespaces()                
                .EnableParameterizedContent()
                .Register(ColorConverter.Default)
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
            var textBox = new TextBox()
            {
                Text =
                    "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23 Sample Text\\par\r\n}"
            };
            var docs = new List<Document>()
            {
                new Document() 
                {
                    Graphics = { textBox }
                }
            };


            serializer.Serialize(stream, documents);
            return Task.CompletedTask;
        }
    }
}
