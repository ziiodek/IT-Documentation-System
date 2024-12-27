using System;
using System.ComponentModel.DataAnnotations;

namespace ITDocumentation
{

    public class Downtime : Author
    {

        public int ID { get; set; }
        public string? SystemImpacted { get; set; }
        public string? Status { get; set; }
        public string? Ticket { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? TimeLapsed { get; set; }
        public string? TimeLapsedMinutes { get; set; }
        public string? Impact { get; set; }
        public string? Cause { get; set; }
        public string? CorrectiveAction { get; set; }
        public string? Requestor { get; set; }
        public string? Notes { get; set; }
        public string? EventType { get; set; }
        public string? Owner { get; set; }
        public string? Date { get; set; }


    }

}