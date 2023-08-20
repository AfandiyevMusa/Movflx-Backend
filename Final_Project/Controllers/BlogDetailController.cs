using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;

        public BlogDetailController(AppDbContext context,
                              IBlogService blogService,
                              ITagService tagService)
        {
            _context = context;
            _blogService = blogService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            Blog blogs = await _blogService.GetByIdAsync(id);
            List<Tag> tags = await _tagService.GetAllById(id);

            BlogDetailVM blogDetailVM = new()
            {
                Blogs = blogs,
                Tags = tags
            };

            return View(blogDetailVM);
        }
    }
}

