using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class Subdepartment : Author
    {

        public int ID { get; set; }

        [Required]
        public int DepartmentID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum character length is 50 characters")]
        public string? Name { get; set; }

    }
}