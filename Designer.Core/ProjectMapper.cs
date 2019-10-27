using AutoMapper;
using Designer.Domain.Models;
using Grace.DependencyInjection;

namespace Designer.Core
{
    public class ProjectMapper : IProjectMapper
    {
        private readonly IMapper mapper;

        public ProjectMapper(ILocatorService locator)
        {
            mapper = new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(locator.Locate);
                MapToDomain(config);
                MapFromDomain(config);
            }).CreateMapper();
        }

        private static void MapFromDomain(IMapperConfigurationExpression config)
        {

            config.CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
                .ConstructUsingServiceLocator();
            config.CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
                .ConstructUsingServiceLocator();

            config.CreateMap<Graphic, Domain.ViewModels.Graphic>(MemberList.Source)
                .IncludeAllDerived();
            config.CreateMap<RectangularGraphic, Domain.ViewModels.RectangularGraphic>(MemberList.Source)
                .IncludeAllDerived();
            config.CreateMap<Field, Domain.ViewModels.Field>(MemberList.Source)
                .IncludeAllDerived();

            config.CreateMap<Rectangle, Domain.ViewModels.Rectangle>(MemberList.Source);
            config.CreateMap<Ellipse, Domain.ViewModels.Ellipse>(MemberList.Source);
            config.CreateMap<Line, Domain.ViewModels.Line>(MemberList.Source);
            config.CreateMap<TextBox, Domain.ViewModels.TextBox>(MemberList.Source);
            config.CreateMap<Image, Domain.ViewModels.Image>(MemberList.Source);
            config.CreateMap<TextField, Domain.ViewModels.TextField>(MemberList.Source);
            config.CreateMap<NumericField, Domain.ViewModels.NumericField>(MemberList.Source);
            config.CreateMap<Shadow, Domain.ViewModels.Shadow>(MemberList.Source);
        }

        private static void MapToDomain(IMapperConfigurationExpression config)
        {
            config.CreateMap<Domain.ViewModels.Project, Domain.Models.Project>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Document, Document>(MemberList.Destination);

            config.CreateMap<Domain.ViewModels.Graphic, Graphic>(MemberList.Source)
                .IncludeAllDerived();
            config.CreateMap<Domain.ViewModels.RectangularGraphic, RectangularGraphic>(MemberList.Source)
                .IncludeAllDerived();
            config.CreateMap<Domain.ViewModels.Field, Field>(MemberList.Source)
                .IncludeAllDerived();

            config.CreateMap<Domain.ViewModels.Rectangle, Rectangle>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Ellipse, Ellipse>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Line, Line>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.TextBox, TextBox>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.NumericField, NumericField>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.TextField, TextField>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Image, Image>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Shadow, Shadow>(MemberList.Destination);
        }

        public Project Map(Domain.ViewModels.Project project)
        {
            return mapper.Map<Project>(project);
        }

        public Domain.ViewModels.Project Map(Project project)
        {
            return mapper.Map<Domain.ViewModels.Project>(project);
        }
    }
}