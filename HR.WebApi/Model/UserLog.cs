using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class UserLog
    {
        [Key]
        public int? UserLog_Id { get; set; }
        public int? User_Id { get; set; }
        public int? Role_Id { get; set; }
        public string User_Name { get; set; }
        public string Host_Name { get; set; }
        public string Ip_Address { get; set; }
        public string App_Id { get; set; }
        public DateTime? AddedOn { get; set; }
    }
}
