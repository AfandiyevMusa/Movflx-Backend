using System;
using Final_Project.Areas.Admin.ViewModels.VideoQuality;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class VideoService: IVideoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public VideoService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<VideoQuality>> GetAllVideoQualities() => await _context.VideoQualities.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<VideoQuality> GetByIdAsync(int? id) => await _context.VideoQualities.FirstOrDefaultAsync(m => m.Id == id);

        public VideoQualityDetailVM GetMappedDatasAsync(VideoQuality dbVideoQuality)
        {
            VideoQualityDetailVM model = new()
            {
                Quality = dbVideoQuality.Quality,
                CreateDate = dbVideoQuality.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            VideoQuality dbVideoQuality = await GetByIdAsync(id);
            _context.VideoQualities.Remove(dbVideoQuality);

            await _context.SaveChangesAsync();
        }
    }
}

