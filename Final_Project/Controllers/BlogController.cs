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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;

        public BlogController(AppDbContext context,
                              IBlogService blogService,
                              ITagService tagService)
        {
            _context = context;
            _blogService = blogService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAllBlogs();
            List<Tag> tags = await _tagService.GetAllTags();

            BlogVM blog = new()
            {
                Blogs = blogs,
                Tags = tags
            };

            return View(blog);
        }
    }
}

