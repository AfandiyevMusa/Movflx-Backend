using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Final_Project.Areas.Admin.ViewModels.Blog;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;
        private readonly ICategoryServices _categoryServices;

        public BlogController(AppDbContext context,
                              IBlogService blogService,
                              ITagService tagService,
                              ICategoryServices categoryServices)
        {
            _context = context;
            _blogService = blogService;
            _tagService = tagService;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            //int take = 3;
            //var paginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);
            //var categoryDb = await _categoryServices.GetAllCategories();
            //var tagDb = await _tagService.GetAllTags();
            //var pageCount = await GetCountAsync(take);

            //ViewBag.count = pageCount;

            //List<ViewModels.BlogVM> mappedDatas = _blogService.GetMappedDatas(paginatedDatas);


            List<Blog> blogs = await _blogService.GetAllBlogs();
            List<Tag> tags = await _tagService.GetAllTags();
            List<Category> categories = await _categoryServices.GetAllCategories();

            //List<BlogVM> blogVMs = await _blogService.GetMappedDatas(paginatedDatas);

            BlogVM blog = new()
            {
                Blogs = blogs,
                Tags = tags,
                Categories = categories
            };

            //Paginate<ViewModels.BlogVM> result = new(mappedDatas, page, pageCount);

            return View(blog);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _blogService.GetCountAsync();

            var res = (int)Math.Ceiling((decimal)count / take);

            return res;
        }
    }
}

