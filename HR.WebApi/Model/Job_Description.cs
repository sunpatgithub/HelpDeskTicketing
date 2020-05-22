using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Job_Description
    {
        [Key]
        public int JD_Id { get; set; }
        
        [Required]
        public int Company_Id { get; set; }

        [Required]
        public int Site_Id { get; set; }

        [Required]
        public int Dept_Id { get; set; }
        
        [Required]
        public int Desig_Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string JD_Code { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w\s.]+\b", ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z), number(0-9), dot(.)")]
        public string JD_Name { get; set; }
        
        [MaxLength(2000)]
        public string JD_Description { get; set; }
        
        [MaxLength(2000)]
        public string Notes { get; set; }

        [MaxLength(1000)]
        public string Path { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must only be 0 or 1.")]
        public Int16 isActive { get; set; }

        [Required]
        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
