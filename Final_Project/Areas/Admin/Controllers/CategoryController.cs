using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Category;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context,
                                  ICategoryServices categoryServices,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _categoryServices = categoryServices;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryVM> list = new();

            List<Category> datas = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new CategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    CreateDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Category category = await _categoryServices.GetByIdAsync(id);
            if (category is null) return NotFound();
            return View(_categoryServices.GetMappedDatasAsync(category));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category category = new()
            {
                Name = request.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Category dbCategory = await _categoryServices.GetByIdAsync(id);
            if (dbCategory is null) return NotFound();

            return View(new CategoryEditVM { Id = dbCategory.Id, Name = dbCategory.Name });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();
            Category dbCategory = await _categoryServices.GetByIdAsync(id);
            if (dbCategory is null) return NotFound();

            dbCategory.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _categoryServices.DeleteAsync((int)id);

            return Ok();
        }
    }
}

