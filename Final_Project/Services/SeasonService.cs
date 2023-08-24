using System;
using Final_Project.Areas.Admin.ViewModels.Seasons;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class SeasonService : ISeasonService
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

        public async Task<Season> GetByIdAsync(int? id) => await _context.Seasons.Include(m => m.Film).Include(m=>m.Episodes).FirstOrDefaultAsync(m => m.Id == id);

        public SeasonDetailVM GetMappedDatasAsync(Season dbSeason)
        {
            SeasonDetailVM model = new()
            {
                Name = dbSeason.Name,
                Film = dbSeason.Film.Name,
                CreatedDate = dbSeason.CreatedDate.ToString("dddd, dd MMMM yyyy"),
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Season dbSeason = await GetByIdAsync(id);

            _context.Seasons.Remove(dbSeason);

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(SeasonCreateVM model)
        {
            Season season = new()
            {
                Name = model.Name,
                FilmId = model.FilmId
            };

            await _context.AddAsync(season);
            await _context.SaveChangesAsync();
        }

        public async Task<Season> GetWithIncludesAsync(int id)
        {
            return await _context.Seasons.Where(m => m.Id == id).Include(m => m.Film).Include(m=>m.Episodes).FirstOrDefaultAsync();
        }

        public async Task EditAsync(int seasonId, SeasonEditVM model)
        {
            var seasons = await GetByIdAsync(seasonId);

            seasons.Name = model.Name;
            seasons.FilmId = model.FilmId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteImageByIdAsync(int id)
        {
            Season season = await _context.Seasons.FirstOrDefaultAsync(m => m.Id == id);
            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Season>> GetAllByIdAsync(int? id) => await _context.Seasons.Include(m => m.Film).Include(m => m.Episodes).Where(m => m.FilmId == id).ToListAsync();

        public List<SeasonVM> GetMappedDatas(List<Season> seasons)
        {
            List<SeasonVM> list = new();
            foreach (var season in seasons)
            {
                list.Add(new SeasonVM
                {
                    Id = season.Id,
                    Name = season.Name,
                    Film = season.Film.Name,
                    FilmImage = season.Film.Images.FirstOrDefault()?.Image,
                    CreatedDate = season.CreatedDate.ToString("dd-MM-yyyy")
                });
            }

            return list;
        }

        public async Task<List<Season>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Seasons.Include(m => m.Film)
                                        .ThenInclude(m=>m.Images)
                                        .Include(m => m.Episodes)
                                        .Skip((page - 1) * take)
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Seasons.CountAsync();
        }
    }
}

