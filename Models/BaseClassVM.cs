using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Models
{
    public class BaseClassVM
    {
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedByIP { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedByIP { get; set; }
        public bool isActive { get; set; } = true;
    }
}
