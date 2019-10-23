using System.Collections.Generic;

namespace Designer.Domain.Models
{
    public class Project
    {
        public IList<Document> Documents { get; set; }
    }
}