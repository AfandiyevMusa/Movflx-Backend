using System;
using Final_Project.Areas.Admin.ViewModels.Category;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class CategoryService: ICategoryServices
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Category>> GetAllCategories() => await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Category> GetByIdAsync(int? id) => await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

        public CategoryDetailVM GetMappedDatasAsync(Category dbCategory)
        {
            CategoryDetailVM model = new()
            {
                Name = dbCategory.Name,
                CreateDate = dbCategory.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Category dbCategory = await GetByIdAsync(id);
            _context.Categories.Remove(dbCategory);

            await _context.SaveChangesAsync();
        }
    }
}

