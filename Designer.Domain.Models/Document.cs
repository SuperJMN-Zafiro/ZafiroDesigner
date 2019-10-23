using System.Collections.Generic;

namespace Designer.Domain.Models
{
    public class Document
    {
        public IList<Graphic> Graphics { get; set; } = new List<Graphic>();
        public string Name { get; set; }
    }
}