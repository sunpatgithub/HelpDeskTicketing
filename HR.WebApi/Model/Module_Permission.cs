using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Module_Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Module_Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9)")]
        public string Name { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must only be 0 or 1.")]
        public Int16 isActive { get; set; }

        [Required]
        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
