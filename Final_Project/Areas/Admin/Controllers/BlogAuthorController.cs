using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.BlogAuthor;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class BlogAuthorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogAuthorService _blogAuthorService;

        public BlogAuthorController(AppDbContext context,
                                    IBlogAuthorService blogAuthorService)
        {
            _context = context;
            _blogAuthorService = blogAuthorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BlogAuthorVM> list = new();

            List<BlogAuthor> datas = await _context.BlogAuthors.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new BlogAuthorVM
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    CreatedDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")

                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            BlogAuthor blogAuthor = await _blogAuthorService.GetByIdAsync(id);
            if (blogAuthor is null) return NotFound();
            return View(_blogAuthorService.GetMappedDatasAsync(blogAuthor));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BlogAuthorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            BlogAuthor blogAuthor = new()
            {
                FullName = request.FullName,
                Age = request.Age,
                Address = request.Address
            };

            await _context.BlogAuthors.AddAsync(blogAuthor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existBlogAuthor = await _blogAuthorService.GetByIdAsync(id);
            if (existBlogAuthor is null) return NotFound();

            BlogAuthorEditVM model = new()
            {
                FullName = existBlogAuthor.FullName,
                Age = existBlogAuthor.Age,
                Address = existBlogAuthor.Address
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogAuthorEditVM request)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existBlogAuthor = await _blogAuthorService.GetByIdAsync(id);
            if (existBlogAuthor is null) return NotFound();

            if (existBlogAuthor.FullName.Trim() == request.FullName.Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            existBlogAuthor.FullName = request.FullName;
            existBlogAuthor.Age = request.Age;
            existBlogAuthor.Address = request.Address;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _blogAuthorService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

