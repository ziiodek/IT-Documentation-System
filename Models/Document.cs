using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class Document : Author
    {
        public int ID { get; set; }

        [Required]
        public int PageID { get; set; }
        [StringLength(200, ErrorMessage = "Maximum character length is 50 characters")]
        public string? Name { get; set; }
        [StringLength(15, ErrorMessage = "Maximum character length is 15 characters")]
        public string? Parent { get; set; }
        [StringLength(30, ErrorMessage = "Maximum character length is 15 characters")]
        public string? Status { get; set; }
        public bool? IsArchive { get; set; }
        public DateTime? ArchiveTime { get; set; }
        public string? ArchiveBy { get; set; }
        public string? ApprovedBy { get; set; }
    }

}