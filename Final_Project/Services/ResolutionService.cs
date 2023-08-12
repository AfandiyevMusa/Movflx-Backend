using System;
using Final_Project.Areas.Admin.ViewModels.Resolution;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class ResolutionService: IResolutionService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ResolutionService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<List<Resolution>> GetAllResolutions() => await _context.Resolutions.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Resolution> GetByIdAsync(int? id) => await _context.Resolutions.FirstOrDefaultAsync(m => m.Id == id);

        public ResolutionDetailVM GetMappedDatasAsync(Resolution dbResolution)
        {
            ResolutionDetailVM model = new()
            {
                ResolutionP = dbResolution.ResolutionP,
                CreateDate = dbResolution.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Resolution dbResolution = await GetByIdAsync(id);
            _context.Resolutions.Remove(dbResolution);

            await _context.SaveChangesAsync();
        }
    }
}

