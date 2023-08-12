using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class FilmService: IFilmService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FilmService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Film>> GetAllFilms() => await _context.Films.Include(m=>m.Resolution).Include(m=>m.FilmTopics).Include(m=>m.Category).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Film> GetByIdAsync(int? id) => await _context.Films.Include(m => m.Resolution).Include(m => m.FilmTopics).Include(m=>m.Category).FirstOrDefaultAsync(m => m.Id == id);
    }
}

