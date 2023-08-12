using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class EpisodeService: IEpisodeService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EpisodeService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Episode>> GetAllEpisodes() => await _context.Episodes.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Episode> GetByIdAsync(int? id) => await _context.Episodes.Include(m => m.Film).Include(m => m.Season).FirstOrDefaultAsync(m => m.Id == id);
    }
}

