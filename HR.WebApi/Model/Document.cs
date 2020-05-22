using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string FileType { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Category { get; set; }
        
        [MaxLength(2000)]
        public string Notes { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;
        
        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}