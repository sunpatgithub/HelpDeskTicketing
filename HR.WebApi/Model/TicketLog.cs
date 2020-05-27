using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
    
    public  class TicketLog
    {
        [Key]
        public int LogId { get; set; }
        public int TicketId { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
       
    }
}
