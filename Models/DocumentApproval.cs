using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class DocumentApproval
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum character length is 50 characters")]
        public string? RequestedBy { get; set; }
        public DateTime DateTime { get; set; }
        public int DocumentID { get; set; }

    }

}