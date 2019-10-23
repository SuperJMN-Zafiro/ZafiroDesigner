using System.Collections.ObjectModel;
using Designer.Domain.ViewModels;
using Grace.DependencyInjection;

namespace Designer.Core
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ILocatorService locatorService;

        public ViewModelFactory(ILocatorService locatorService)
        {
            this.locatorService = locatorService;
        }

        public Project CreateProject()
        {
            var project = locatorService.Locate<Project>();
            var document = CreateDocument();
            project.Documents.Add(document);
            project.SelectedDocument = document;
            return project;
        }

        public Document CreateDocument()
        {
            var document = locatorService.Locate<Document>();
            document.Name = "New document";
            document.Graphics = new ObservableCollection<Graphic>();
            return document;
        }
    }
}