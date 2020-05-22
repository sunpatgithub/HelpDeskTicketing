using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class User_Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public int Role_Id { get; set; }

        [RegularExpression(@"\b[0-1]{1}\b", ErrorMessage = "Value must be 0 or 1.")]
        public Int16 isActive { get; set; }
        
        [Required]
        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
