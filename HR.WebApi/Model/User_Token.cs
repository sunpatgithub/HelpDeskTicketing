using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User_Token
    {
        [Key]
        public int UserToken_Id { get; set; }
        
        public int? User_Id { get; set; }
        
        public string Token_No { get; set; }
        
        public DateTime? Token_ExpiryDate { get; set; }
        
        public int? Attempted { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }

        public int? AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
