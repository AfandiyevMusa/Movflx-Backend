using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.Areas.Admin.ViewModels.Episode;
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

        public async Task<List<Episode>> GetAllEpisodes() => await _context.Episodes.Include(m=>m.Film).Include(m=>m.Season).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Episode> GetByIdAsync(int? id) => await _context.Episodes.Include(m => m.Film).Include(m => m.Season).FirstOrDefaultAsync(m => m.Id == id);

        public EpisodeDetailVM GetMappedData(Episode episode)
        {
            EpisodeDetailVM episodeDetail = new()
            {
                Name = episode.Name,
                Film = episode.Film.Name,
                Season = episode.Season.Name,
                CreatedDate = episode.CreatedDate.ToString("dddd, dd MMMM yyyy")
            };
            return episodeDetail;
        }

        public async Task DeleteAsync(int id)
        {
            Episode dbEpisode = await GetByIdAsync(id);

            _context.Episodes.Remove(dbEpisode);

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(EpisodeCreateVM model)
        {
            Episode episode = new()
            {
                Name = model.Name,
                FilmId = model.FilmId,
                SeasonId = model.SeasonId
            };

            await _context.AddAsync(episode);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int episodeId, EpisodeEditVM model)
        {
            Episode episode = await GetByIdAsync(episodeId);

            episode.FilmId = model.FilmId;
            episode.SeasonId = model.SeasonId;
            episode.Name = model.Name;

            _context.Update(episode);
            await _context.SaveChangesAsync();
        }

        public async Task<Episode> GetByIdWithAllIncludesAsync(int? id)
        {
            return await _context.Episodes.Include(m => m.Season).
                                           Include(m => m.Film).
                                           FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Episode>> GetAllByIdAsync(int? id) => await _context.Episodes.Include(m => m.Film).Include(m => m.Season).Where(m => m.Id == id).ToListAsync();
    }
}

