using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ITDocumentation
{
    public class Application:Author
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string LogonMethod { get; set; }
        public string UsersFrom{ get; set; }
        public string Owner { get; set; }
        public string Tech { get; set; }
        public string Notes { get; set; }
  
    }
}
