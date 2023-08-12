using System;
using Final_Project.Areas.Admin.ViewModels.BlogAuthor;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class BlogAuthorService : IBlogAuthorService
    {
        private readonly AppDbContext _context;

        public BlogAuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BlogAuthor> GetByIdAsync(int? id) => await _context.BlogAuthors.FirstOrDefaultAsync(m => m.Id == id);

        public BlogAuthorDetailVM GetMappedDatasAsync(BlogAuthor dbBlogAuthor)
        {
            BlogAuthorDetailVM model = new()
            {
                FullName = dbBlogAuthor.FullName,
                Age = dbBlogAuthor.Age,
                Address = dbBlogAuthor.Address,
                CreatedDate = dbBlogAuthor.CreatedDate.ToString("dd-MM-yyyy"),
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            BlogAuthor dbBlogAuthor = await GetByIdAsync(id);

            _context.BlogAuthors.Remove(dbBlogAuthor);

            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogAuthor>> GetAll() => await _context.BlogAuthors.ToListAsync();
    }
}

