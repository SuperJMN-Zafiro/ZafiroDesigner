using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Designer.Domain.Models;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using Grace.DependencyInjection;


namespace Designer.Core.Persistence
{
    public class ProjectStore : IProjectStore
    {
        private readonly IExtendedXmlSerializer serializer;
        private readonly IMapper mapper;

        public ProjectStore(ILocatorService locator)
        {
            serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableParameterizedContent()
                .Register(ColorConverter.Default)
                .Create();

            mapper = new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(locator.Locate);


                config.CreateMap<Domain.ViewModels.Project, Domain.Models.Project>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Document, Document>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Graphic, Graphic>(MemberList.Destination)
                    .IncludeAllDerived();
                config.CreateMap<Domain.ViewModels.Rectangle, Rectangle>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Ellipse, Ellipse>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Line, Line>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.TextBox, TextBox>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Field, Field>(MemberList.Destination);
                config.CreateMap<Domain.ViewModels.Image, Image>(MemberList.Destination);
                
                config.CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
                    .ConstructUsingServiceLocator();
                config.CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
                    .ConstructUsingServiceLocator();
                config.CreateMap<Graphic, Domain.ViewModels.Graphic>(MemberList.Source)
                    .IncludeAllDerived();
                config.CreateMap<Rectangle, Domain.ViewModels.Rectangle>(MemberList.Source);
                config.CreateMap<Ellipse, Domain.ViewModels.Ellipse>(MemberList.Source);
                config.CreateMap<Line, Domain.ViewModels.Line>(MemberList.Source);
                config.CreateMap<TextBox, Domain.ViewModels.TextBox>(MemberList.Source);
                config.CreateMap<Field, Domain.ViewModels.Field>(MemberList.Source);
                config.CreateMap<Image, Domain.ViewModels.Image>(MemberList.Source);

            }).CreateMapper();
        }

        public Task<Domain.ViewModels.Project> Load(Stream stream)
        {
            var model = serializer.Deserialize<Project>(stream);
            var viewModel = mapper.Map<Domain.ViewModels.Project>(model);
            return Task.FromResult(viewModel);
        }

        public Task Save(Domain.ViewModels.Project project, Stream stream)
        {
            var model = mapper.Map<Project>(project);
            serializer.Serialize(stream, model);
            return Task.CompletedTask;
        }
    }
}