using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


namespace ITDocumentation
{
    public class Author
    {
        [StringLength(30)]
        public string? AuthorName {get; set;}
        public DateTime DateTime {get;set;}
        public string? ModifiedBy { get; set;}
        public DateTime? ModifiedDate { get; set; }


       
    }
}

