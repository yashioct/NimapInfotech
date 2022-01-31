using nimapInfotech.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Models
{
    public class ProductMasterVM : BaseClassVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [ForeignKey("CategoryMaster")]
        [Required]
        public int DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        public virtual CategoryMaster CategoryMaster { get; set; }
    }
}
