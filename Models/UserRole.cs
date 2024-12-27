using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class UserRole
    {

        public int ID { get; set; }

        [Required]
        public string? Username { get; set; }
        public string? RoleName { get; set; }
        public string? Name { get; set; }
        public int DepartmentID { get; set; }

    }

}