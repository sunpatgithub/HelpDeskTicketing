using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }
        [Required]
        public int Company_Id { get; set; }
        [Required]
        public int Site_Id { get; set; }
        [Required]
        public int JD_Id { get; set; }
        [Required]
        public int Dept_Id { get; set; }
        [Required]
        public int Desig_Id { get; set; }
        [Required]
        public int Zone_Id { get; set; }
        [Required]
        public int Shift_Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[\w-]+\b",ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9), Dash(-)")]
        public string Emp_Code { get; set; }
        
        [Required]
        public DateTime JoiningDate { get; set; }
        public int? Reporting_Id { get; set; }
        public Int16? isSponsored { get; set; }
        public Int16? Tupe { get; set; }
       
        [MaxLength(100)]
        public string NiNo { get; set; }
        
        [MaxLength(1000)]
        public string NiCategory { get; set; }
        public int? PreviousEmp_Id { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

       
    }
}
