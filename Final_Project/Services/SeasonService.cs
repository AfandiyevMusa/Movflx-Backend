using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class SeasonService: ISeasonService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeasonService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Season>> GetAllSeasons() => await _context.Seasons.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Season> GetByIdAsync(int? id) => await _context.Seasons.Include(m => m.Film).FirstOrDefaultAsync(m => m.Id == id);
    }
}

