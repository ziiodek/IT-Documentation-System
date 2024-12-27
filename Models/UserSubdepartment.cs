using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class UserSubdepartment
    {
        public int ID { get; set; }
        [Required]
        public string? Username { get; set; }
        public int SubdepartmentID { get; set; }
        public int DepartmentID { get; set; }

    }

}