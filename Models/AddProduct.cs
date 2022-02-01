using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Models
{
    public class AddProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryID { get; set; }
        public List<SelectListItem>? CategoryMasterList { get; set; }

    }
}