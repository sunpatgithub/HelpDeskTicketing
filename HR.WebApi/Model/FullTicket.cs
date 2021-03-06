﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Model
{

    public class FullTicket
    {
        [Key]
        public int TicketId { get; set; }
        public string RequesterId { get; set; }
        public string Requester { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int DeptId { get; set; }
        public string DepartmentName { get; set; }
        public string AssignToId { get; set; }
        public string AssignToName { get; set; }
        public Int16 IsActive { get; set; }
        public string AddedById { get; set; }
        public string AddedByName { get; set; }
        public DateTime? AddedOn { get; set; }
        public string UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        

    }
}
