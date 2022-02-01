using nimapInfotech.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Models
{
    public class CategoryMasterVM : BaseClassVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }

}
