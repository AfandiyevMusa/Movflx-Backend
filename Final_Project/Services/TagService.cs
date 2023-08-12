using System;
using Final_Project.Areas.Admin.ViewModels.Tag;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class TagService: ITagService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TagService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Tag>> GetAllTags() => await _context.Tags.Include(m=>m.BlogTags).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Tag> GetByIdAsync(int? id) => await _context.Tags.Include(m => m.BlogTags).FirstOrDefaultAsync(m => m.Id == id);

        public TagDetailVM GetMappedDatasAsync(Tag dbTag)
        {
            TagDetailVM model = new()
            {
                Name = dbTag.Name,
                CreateDate = dbTag.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Tag dbTag= await GetByIdAsync(id);
            _context.Tags.Remove(dbTag);

            await _context.SaveChangesAsync();
        }
    }
}

