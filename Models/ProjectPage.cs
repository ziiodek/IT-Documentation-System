using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class ProjectPage : Author
    {

        public int ID { get; set; }

        [Required]
        public int ProjectID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum Chacter length is 50 characters")]
        public string Name { get; set; }
        public string PageContent { get; set; }
    }

}