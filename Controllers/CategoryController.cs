using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nimapInfotech.Entity;
using nimapInfotech.Models;
using nimapInfotech.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NimapInfotechContext dbContext;
        public CategoryController(NimapInfotechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategory model)
        {
            try
            {
                return PartialView("_AddCategory", model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoadCategoryMaster()
        {
            try
            {
                List<CategoryMaster> data = new List<CategoryMaster>();
                string draw, start, length, sortColumn, sortColumnDir, search, col1;
                draw = Request.Form.FirstOrDefault(x => x.Key == "draw").Value.FirstOrDefault();
                start = Request.Form.FirstOrDefault(x => x.Key == "start").Value.FirstOrDefault();
                length = Request.Form.FirstOrDefault(x => x.Key == "length").Value.FirstOrDefault();
                col1 = Request.Form.FirstOrDefault(x => x.Key == "order[0][column]").Value.FirstOrDefault();
                sortColumn = Request.Form.FirstOrDefault(x => x.Key == "columns[" + col1 + "]").Value.FirstOrDefault();
                sortColumnDir = Request.Form.FirstOrDefault(x => x.Key == "order[0][dir]").Value.FirstOrDefault();
                search = Request.Form.FirstOrDefault(x => x.Key == "search[value]").Value.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int totalRecord = 0;
                int filteredRecord = 0;
                JsonResult json;
                data = await this.dbContext.CategoryMaster.ToListAsync();
               

                filteredRecord = totalRecord;

                return Ok(new
                {
                    draw,
                    recordsFilterd = filteredRecord,
                    recordsTotal = totalRecord,
                    iTotalRecords = totalRecord,
                    iTotalDisplayRecords = filteredRecord,
                    iDisplayStart = start,
                    data
                });
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
    

