using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Blog;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IBlogAuthorService _blogAuthorService;
        private readonly IWebHostEnvironment _env;
        private readonly ITagService _tagService;

        public BlogController(AppDbContext context,
                              IBlogService blogService,
                              IBlogAuthorService blogAuthorService,
                              IWebHostEnvironment env,
                              ITagService tagService)
        {
            _context = context;
            _blogAuthorService = blogAuthorService;
            _env = env;
            _blogService = blogService;
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BlogVM> list = new();

            List<Blog> datas = await _context.Blogs.Include(m => m.BlogImages).Include(m => m.BlogAuthor).OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                string blogImage = item.BlogImages.FirstOrDefault()?.Image;

                list.Add(new BlogVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    BlogImage = blogImage,
                    BlogAuthor = item.BlogAuthor?.FullName,
                    CreatedDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });
            }


            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog = await _blogService.GetByIdAsync(id);
            if (blog is null) return NotFound();
            return View(_blogService.GetMappedDatasAsync(blog));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllAuthors();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            await GetAllAuthors();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            foreach (var item in request.BlogImages)
            {
                if (!item.CheckFileType("image"))
                {
                    ModelState.AddModelError("BlogImages", "Please select only image file.");
                    return View(request);
                }

                if (item.CheckFileSize(20000))
                {
                    ModelState.AddModelError("BlogImages", "Please select under 200KB image");
                    return View(request);
                }
            }

            var tags = await _tagService.GetAllTags();

            await _blogService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var blog = await _blogService.GetByIdAsync(id);
            if (blog is null) return NotFound();

            await GetAllAuthors();

            BlogEditVM model = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                BlogAuthorId = blog.BlogAuthorId,
                BlogImages = blog.BlogImages.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(BlogEditVM request, int? id)
        {
            if (id is null) return BadRequest();
            await GetAllAuthors();
            var blog = await _blogService.GetByIdAsync(id);
            if (blog is null) return NotFound();

            if (ModelState.IsValid)
            {
                request.BlogImages = blog.BlogImages.ToList();
                return View();
            }

            if (request.NewBlogImages != null)
            {
                foreach (var item in request.NewBlogImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewBlogImages", "Please select only image file");
                        request.BlogImages = blog.BlogImages.ToList();
                        return View();
                    }


                    if (item.CheckFileSize(200))
                    {
                        ModelState.AddModelError("NewBlogImages", "Image size must be max 200 KB");
                        request.BlogImages = blog.BlogImages.ToList();
                        return View();
                    }
                }
            }

            await _blogService.EditAsync(blog.Id, request);

            return RedirectToAction(nameof(Index));
        }


        private async Task GetAllAuthors()
        {
            ViewBag.blogAuthors = await GetBlogAuthors();
        }

        private async Task<SelectList> GetBlogAuthors()
        {
            List<BlogAuthor> blogAuthors = await _blogAuthorService.GetAll();
            return new SelectList(blogAuthors, "Id", "FullName");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _blogService.DeleteAsync((int)id);

            return Ok();
        }

        [HttpPost]
        public async Task DeleteBlogImage(int id)
        {
            await _blogService.DeleteImageByIdAsync(id);
        }
    }
}

