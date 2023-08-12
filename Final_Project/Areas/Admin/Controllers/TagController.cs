using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Tag;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        private readonly IWebHostEnvironment _env;

        public TagController(AppDbContext context,
                                  ITagService tagService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _tagService = tagService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TagVM> list = new();

            List<Tag> datas = await _context.Tags.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new TagVM
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
            Tag tag = await _tagService.GetByIdAsync(id);
            if (tag is null) return NotFound();
            return View(_tagService.GetMappedDatasAsync(tag));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(TagCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Tag tag = new()
            {
                Name = request.Name
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Tag dbTag = await _tagService.GetByIdAsync(id);
            if (dbTag is null) return NotFound();

            return View(new TagEditVM { Id = dbTag.Id, Name = dbTag.Name });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, TagEditVM request)
        {
            if (id is null) return BadRequest();
            Tag dbTag = await _tagService.GetByIdAsync(id);
            if (dbTag is null) return NotFound();

            dbTag.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _tagService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

