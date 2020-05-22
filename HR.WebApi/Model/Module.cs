using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w]+\b", ErrorMessage = "Value must contain any of the following without space: upper case (A-Z), lower case (a-z), number(0-9),Underscore(_)")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"\b[\w\s.]+\b", ErrorMessage = "Value must contain any of the following: upper case (A-Z), lower case (a-z), number(0-9), dot(.)")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must only be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
