using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Model
{
    public class Employee_BasicInfo
    {
        [Key]
        public int BasicInfo_Id { get; set; }
        [Required]
        public int Emp_Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[a-zA-Z\s]+\b",ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [RegularExpression(@"\b[a-zA-Z\s]+\b",ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[a-zA-Z\s]+\b",ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }
        //public string Photo { get; set; }
        public DateTime? DOB { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[a-zA-Z]+\b",ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string Gender { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string BloodGroup { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"\b[a-zA-Z]+\b",ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string Nationality { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Ethnicity_Code { get; set; }
        
        [MaxLength(20)]
        public string Version_Id { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
