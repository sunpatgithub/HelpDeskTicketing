using HR.WebApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.ModelView
{
    public class EmployeeView
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
        [RegularExpression(@"^(([A-za-z0-9-]+[\s]{1}[A-za-z0-9-]+)|([A-Za-z0-9-]+))$", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9), Dash(-)")]
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

        //public IEnumerable<Employee_Address> emp_address { get; set; }
        //public IEnumerable<Employee_Bank> emp_bank { get; set; }

        //[Required]
        //public Employee_BasicInfo emp_basicinfo { get; set; }
        //public IEnumerable<Employee_Contact> emp_contact { get; set; }
        //public IEnumerable<Employee_Contract> emp_contract { get; set; }
        //public IEnumerable<Employee_Document> emp_document { get; set; }
        //public IEnumerable<Employee_Emergency> emp_emergency { get; set; }
        //public IEnumerable<Employee_Probation> emp_probation { get; set; }
        //public IEnumerable<Employee_Reference> emp_reference { get; set; }
        //public IEnumerable<Employee_Resignation> emp_resignation { get; set; }
        //public IEnumerable<Employee_RightToWork> emp_righttowork { get; set; }
        //public IEnumerable<Employee_Salary> emp_salary { get; set; }
    }
}