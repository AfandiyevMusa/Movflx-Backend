using System;
using Final_Project.Areas.Admin.ViewModels.Category;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface ICategoryServices
	{
        Task<List<Category>> GetAllCategories();
        Task<Category> GetByIdAsync(int? id);
        CategoryDetailVM GetMappedDatasAsync(Category dbCategory);
        Task DeleteAsync(int id);
    }
}

