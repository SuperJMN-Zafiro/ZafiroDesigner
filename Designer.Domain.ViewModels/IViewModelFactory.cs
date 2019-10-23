namespace Designer.Domain.ViewModels
{
    public interface IViewModelFactory
    {
        Project CreateProject();
        Document CreateDocument();
    }
}