using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Model
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public Int16 isActive { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
