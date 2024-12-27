using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ITDocumentation.Classes
{
    public class SearchResult : Author
    {
        public int ID {get;set;}
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Status { get; set; }
        public string? ApprovedBy { get; set; }

    }
}
