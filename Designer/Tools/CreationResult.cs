using Designer.Model;

namespace Designer.Tools
{
    public class CreationResult
    {
        public Graphic Graphic { get; }
        public bool IsSuccessful { get; } = true;

        public CreationResult(Graphic graphic)
        {
            Graphic = graphic;
        }

        public CreationResult()
        {
            IsSuccessful = false;
        }
    }
}