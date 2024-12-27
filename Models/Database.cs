using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{
    public class Database : Author
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string SQLInstance { set; get; }

    }
}
