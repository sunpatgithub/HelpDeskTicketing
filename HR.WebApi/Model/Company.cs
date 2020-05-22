using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Company
    {
        [Key]
        public int Company_Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string Company_Code { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w\s.]+\b", ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z), number(0-9), dot(.)")]
        public string Company_Name { get; set; }
        
        public int? Company_Parent_Id { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string Registration_No { get; set; }
        
        public DateTime? Registration_Date { get; set; }
        
        public string Logo { get; set; }

        public string Currency { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w\s]+\b", ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string Language { get; set; }
        
        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }

        //public IEnumerable<Company_Contact> company_contact { get; set; }           
    }
}