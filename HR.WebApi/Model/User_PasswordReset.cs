using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User_PasswordReset
    {
        [Key]
        public int User_PasswordReset_Id { get; set; }
        
        public int? User_Id { get; set; }
        
        public DateTime? PasswordReset_ExpiryDate { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string PasswordReset_Link { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string PasswordReset_Status { get; set; }
        
        //[StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Token_No { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
