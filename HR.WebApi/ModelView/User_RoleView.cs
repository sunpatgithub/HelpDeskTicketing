using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class User_RoleView : Pagination
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public int Role_Id { get; set; }
        public Int16 isActive { get; set; }
        [Required]
        public int AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }

        public string Login_Id { get; set; }
        public string Name { get; set; }
    }
}
