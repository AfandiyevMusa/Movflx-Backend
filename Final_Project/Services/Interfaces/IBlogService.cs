using System;
using Final_Project.Areas.Admin.ViewModels.Blog;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IBlogService
	{
        Task<List<Blog>> GetAllBlogs();
        Task<Blog> GetByIdAsync(int? id);
        BlogDetailVM GetMappedDatasAsync(Blog dbBlog);
        Task DeleteAsync(int id);
        Task CreateAsync(BlogCreateVM model);
        Task<Blog> GetWithIncludesAsync(int id);
        Task EditAsync(int blogId, BlogEditVM model);
        Task DeleteImageByIdAsync(int id);
    }
}

