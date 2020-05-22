using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        
        [Required]
        public int Company_Id { get; set; }

        public int? Emp_Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z0-9.@_-]{3,100}$",
            ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z), number(0-9) and special character(e.g. .@_-)")]
        //Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number(0-9) and special character(e.g. !@#$%^&*)
        public string Login_Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"\b[a-zA-Z\s]+\b",
            ErrorMessage = "Value must contain any of the following : upper case (A-Z), lower case (a-z)")]
        public string User_Type { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        public DateTime? PasswordExpiryDate { get; set; }

        public int? Attempted { get; set; }

        public int? isLocked { get; set; }

        public int? LockExpiryTime { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }

        public int? AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
