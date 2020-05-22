using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Task_Activity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Emp_Id { get; set; }
        
        //[MaxLength(500)]
        public string Description { get; set; }
        
        //[Required]
        //[MaxLength(100)]
        public string Activity { get; set; }
        
        public string Notes { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[A-Za-z\s]+\b", ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z)")]
        public string Status { get; set; }
        
        public int? AssignTo { get; set; }
        
        public int AssignDept { get; set; }
 
        [EmailAddress]
        public string AssignEmail { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
 
        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}