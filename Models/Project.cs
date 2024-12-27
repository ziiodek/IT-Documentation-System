using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class Project : Author
    {

        public int ID { get; set; }
        public int SubdepartmentID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum Chacter length is 50 characters")]
        public string? Name { get; set; }
    }

}