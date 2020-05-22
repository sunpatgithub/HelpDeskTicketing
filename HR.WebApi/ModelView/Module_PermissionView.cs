using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class Module_PermissionView
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Module_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public Int16 isActive { get; set; }
        [Required]
        public int AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public string Module_Name { get; set; }
    }
}
