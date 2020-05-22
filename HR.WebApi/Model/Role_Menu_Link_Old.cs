using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Role_Menu_Link_Old
    {
        [Key]
        public int Role_Menu_Link_Id { get; set; }
        
        [Required]
        public int Role_Id { get; set; }
        
        [Required]
        public int Menu_Id { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must only be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }
        
        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}
