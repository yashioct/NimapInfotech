using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using nimapInfotech.Entity;
using nimapInfotech.Models;
using nimapInfotech.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace nimapInfotech.Controllers
{
    public class ProductController : Controller
    {
        private readonly NimapInfotechContext dbContext;
       
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, NimapInfotechContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProduct model)
        {
            model.CategoryMasterList = new List<SelectListItem>() { };
            var categoryMasterList = await this.dbContext.CategoryMaster.Where(f => f.isActive == true).ToListAsync();
            if (categoryMasterList != null)
            {
                foreach (var item in categoryMasterList)
                {
                    model.CategoryMasterList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = model?.CategoryID == item.Id
                    });
                }

            }
            return PartialView("_AddProduct", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddProduct(AddProduct model)
        {
            bool isSuccess = false;
            if (model.Id > 0)
            {
                var data = await this.dbContext.ProductMaster.FirstOrDefaultAsync(f => f.Id == model.Id);
                if (data != null)
                {
                    data.Name = model.Name;
                    data.CategoryId = model.CategoryID;
                    data.ModifiedBy = "yashi";
                    data.ModifiedOn = DateTime.Now;
                    this.dbContext.ProductMaster.Update(data);
                }
            }
            else
            {
                var obj = new ProductMaster()
                {
                    Name = model.Name,
                    CategoryId = model.CategoryID,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "yashi"
                };
                await this.dbContext.ProductMaster.AddAsync(obj); ;
            }

            int record = await this.dbContext.SaveChangesAsync();
            isSuccess = record > 0;

            return Json(new { success = isSuccess, model });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool isSuccess = false;
            if (id > 0)
            {
                var data = await this.dbContext.ProductMaster.FirstOrDefaultAsync(f => f.Id == id);
                if (data != null)
                {
                    data.isActive = false;
                    data.ModifiedBy = "yashi";
                    data.ModifiedOn = DateTime.Now;
                    this.dbContext.ProductMaster.Update(data);
                }
            }

            int record = await this.dbContext.SaveChangesAsync();
            isSuccess = record > 0;

            return Json(new { success = isSuccess });
        }

        [HttpPost]
        public async Task<IActionResult> LoadProductMaster()
        {
            try
            {
                List<ProductMaster> data = new List<ProductMaster>();
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
                data = await this.dbContext.ProductMaster.Include(c => c.CategoryMaster).Where(f => f.isActive == true).ToListAsync();
                foreach (var item in data)
                {
                    item.CategoryMasterName = item?.CategoryMaster?.Name;
                }
                if (string.IsNullOrWhiteSpace(search))
                {
                    var res = data.AsQueryable();
                    var v = (from item in res select item);
                    filteredRecord = v.ToList().Count();
                    data = pageSize == -1 ? v.ToList() : v.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    var res = data.AsEnumerable().Where(p => (p.Name != null && p.Name.ToLower().Contains(search)
                    || p.CategoryMasterName != null && p.CategoryMasterName.ToLower().Contains(search)
                    || p.CategoryId.ToString().Contains(search)
                    || p.Id.ToString().Contains(search)
                    )).AsQueryable();
                    var v = (from item in res select item);
                    filteredRecord = v.ToList().Count();
                    data = pageSize == -1 ? v.ToList() : v.Skip(skip).Take(pageSize).ToList();
                }

                totalRecord = filteredRecord;

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
    

