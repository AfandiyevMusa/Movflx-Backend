using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Final_Project.ViewModels;
using Final_Project.Data;
using Final_Project.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class CommonController : Controller
    {
        private readonly AppDbContext _context;

        public CommonController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult SearchMovie(string search)
        {
            SearchPartialVM searchPartialVM = new SearchPartialVM();
            if (!String.IsNullOrEmpty(search))
            {
                searchPartialVM.Films = _context.Films.Where(m => m.Name.ToLower().Contains(search.ToLower())).Take(2).OrderByDescending(n => n.Id).ToList();
            }

            return PartialView("_SearchMoviePartialView", searchPartialVM);
        }

        public IActionResult SearchBlog(string search)
        {
            SearchPartialVM searchPartialVM = new SearchPartialVM();
            if (!String.IsNullOrEmpty(search))
            {
                searchPartialVM.Blogs = _context.Blogs.Where(m => m.Title.ToLower().Contains(search.ToLower())).Take(2).OrderByDescending(n => n.Id).ToList();
            }

            return PartialView("_SearchBlogPartialView", searchPartialVM);
        }
    }
}

