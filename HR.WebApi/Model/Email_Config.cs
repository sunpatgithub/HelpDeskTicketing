using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Email_Config
    {
        [Key]
        public int Email_Config_Id { get; set; }

        [Required]
        public int Company_Id { get; set; }

        [Required]
        //[RegularExpression(@"^(([A-za-z0-9.@]+[\s]{1}[A-za-z0-9.@]+)|([A-Za-z0-9.@]+)){5,500}$")]
        public string Email_Host { get; set; }

        [Required]
        public int Email_Port { get; set; }

        [Required]
        public string Email_UserName { get; set; }

        [Required]
        public string Email_Password { get; set; }

        [Required]
        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 EnableSSL { get; set; }

        public Int16 TLSEnable { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;
        
        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}
