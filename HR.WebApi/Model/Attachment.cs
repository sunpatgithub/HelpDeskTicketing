using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
   
    public class Attachment
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int TicketId { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
