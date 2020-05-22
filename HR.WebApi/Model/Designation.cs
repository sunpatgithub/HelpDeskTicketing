using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Designation
    {
        [Key]
        public int Desig_Id { get; set; }

        [Required]
        public int Company_Id { get; set; }
        
        [Required]
        public int Dept_Id { get; set; }
        
        [Required]
        public int Zone_Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string Desig_Code { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w\s.]+\b", ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z), number(0-9), dot(.)")]
        public string Desig_Name { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Report_To { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string AdditionalReport_To { get; set; }
        
        public int? Level { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }

        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;
        
        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}