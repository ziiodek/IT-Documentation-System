using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class SinglePage : Author
    {

        public int ID { get; set; }
        public int SubdepartmentID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum character length is 50 characters")]
        public string? Name { get; set; }
        public string? PageContent { get; set; }
    }


}