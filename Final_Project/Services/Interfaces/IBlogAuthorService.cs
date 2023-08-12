using System;
using Final_Project.Areas.Admin.ViewModels.BlogAuthor;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IBlogAuthorService
	{
        Task<List<BlogAuthor>> GetAll();
        Task<BlogAuthor> GetByIdAsync(int? id);
        BlogAuthorDetailVM GetMappedDatasAsync(BlogAuthor dbBlogAuthor);
        Task DeleteAsync(int id);
    }
}

