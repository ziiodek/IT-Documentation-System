using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ITDocumentation
{
    public class Server : Author
    {
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Ip { get; set; }
        public string Status { get; set; }
        public string PatchedBy { get; set; }
        public string DatePatched { get; set; }
        public string AddExclusions { get; set; }
        public string TaegisAgent { get; set;}
        public string DUO { get; set; }
        public string Notes { get; set; }

    }
}
