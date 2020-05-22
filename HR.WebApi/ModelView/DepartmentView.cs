using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.ModelView
{
    public class DepartmentView: Pagination
    {
        [Key]
        public int Dept_Id { get; set; }
        [Required]
        public int Company_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Dept_Code { get; set; }
        [Required]
        [MaxLength(500)]
        public string Dept_Name { get; set; }
        [MaxLength(1000)]
        public string Notes { get; set; }

        public Int16 isActive { get; set; }
        [Required]
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Company_Name { get; set; }
    }
}
