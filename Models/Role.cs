using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class Role
    {

        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum character length is 50 characters")]
        public string? RoleName { get; set; }

    }

}