namespace Designer.Domain.ViewModels
{
    public class CreationResult
    {
        public Graphic Node { get; }
        public bool IsSuccessful { get; set; } = true;

        public CreationResult(Graphic node)
        {
            Node = node;
        }

        public CreationResult()
        {
            IsSuccessful = false;
        }
    }
}