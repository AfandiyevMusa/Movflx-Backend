using System;
using Final_Project.Areas.Admin.ViewModels.Blog;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Blog>> GetAllBlogs() => await _context.Blogs.Include(m => m.BlogImages).Include(m => m.BlogAuthor).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Blog> GetByIdAsync(int? id) => await _context.Blogs.Include(m => m.BlogImages).Include(m => m.BlogAuthor).FirstOrDefaultAsync(m => m.Id == id);

        public BlogDetailVM GetMappedDatasAsync(Blog dbBlog)
        {
            BlogDetailVM model = new()
            {
                Title = dbBlog.Title,
                Description = dbBlog.Description,
                BlogImages =
                dbBlog.BlogImages,
                BlogAuthor = dbBlog.BlogAuthor.FullName,
                CreateDate = dbBlog.CreatedDate.ToString("dddd, dd MMMM yyyy"),
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Blog dbBlog = await GetByIdAsync(id);

            _context.Blogs.Remove(dbBlog);

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(BlogCreateVM model)
        {
            List<BlogImage> images = new();

            foreach (var item in model.BlogImages)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "/assets/img/blog/");

                images.Add(new BlogImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Blog blog = new()
            {
                Title = model.Title,
                Description = model.Description,
                BlogImages = images,
                BlogAuthorId = model.BlogAuthorId
            };

            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> GetWithIncludesAsync(int id)
        {
            return await _context.Blogs.Where(m => m.Id == id).Include(m => m.BlogImages).Include(m => m.BlogAuthor).FirstOrDefaultAsync();
        }

        public async Task EditAsync(int blogId, BlogEditVM model)
        {
            List<BlogImage> images = new();

            var blog = await GetByIdAsync(blogId);

            if (model.NewBlogImages != null)
            {
                foreach (var item in model.NewBlogImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "/assets/img/blog/");
                    images.Add(new BlogImage { Image = fileName, BlogId = blogId });
                }

                await _context.BlogImages.AddRangeAsync(images);
            }
            else
            {
                blog.Title = model.Title;
                blog.Description = model.Description;
                blog.BlogImages = model.BlogImages;
                blog.BlogAuthorId = model.BlogAuthorId;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteImageByIdAsync(int id)
        {
            BlogImage blogImage = await _context.BlogImages.FirstOrDefaultAsync(m => m.Id == id);
            _context.BlogImages.Remove(blogImage);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath + "/assets/img/blog/"+ blogImage.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}

