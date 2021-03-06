﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{
  
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string RequesterId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CatId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int DeptId { get; set; }
        public string AssignToId { get; set; }
        //public byte? IsActive { get; set; }
        public Int16 IsActive { get; set; }
        public string AddedById { get; set; }
        public DateTime? AddedOn { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public int Assignee { get; set; }
        //public int Cat { get; set; }
        //public int Dept { get; set; }
        //public int Requester { get; set; }
        //public ICollection<Attachment> Attachment { get; set; }
        
    }
}
