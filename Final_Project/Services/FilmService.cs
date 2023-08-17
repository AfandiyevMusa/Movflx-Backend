using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Final_Project.Areas.Admin.ViewModels.Movie;
using Final_Project.Helpers;

namespace Final_Project.Services
{
	public class FilmService: IFilmService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IFilmTopicService _filmTopicService;

        public FilmService(AppDbContext context,
                           IWebHostEnvironment env,
                           IFilmTopicService filmTopicService)
        {
            _context = context;
            _env = env;
            _filmTopicService = filmTopicService;
        }

        public async Task<List<Film>> GetAllFilmsAsync() => await _context.Films.Include(m=>m.Resolution).Include(m=>m.FilmTopics).Include(m=>m.Category).Include(m=>m.Images).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Film> GetByIdAsync(int? id) => await _context.Films.Include(m => m.Resolution).Include(m => m.FilmTopics).ThenInclude(m => m.Topic).Include(m=>m.Category).Include(m=>m.Images).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Film> GetByIdWithAllIncludesAsync(int? id)
        {
            return await _context.Films.Include(m => m.Resolution).
                                           Include(m => m.Images).
                                           Include(m => m.Category).
                                           Include(m => m.FilmTopics).
                                           ThenInclude(m=>m.Topic).
                                           Include(m => m.Episodes).
                                           Include(m => m.Seasons).
                                           FirstOrDefaultAsync(m => m.Id == id);
        }


        public MovieDetailVM GetMappedData(Film film)
        {
            MovieDetailVM movieDetail = new()
            {
                Name = film.Name,
                Images = film.Images,
                MinAge = film.MinAge,
                Duration = film.Duration,
                Resolution = film.Resolution.ResolutionP,
                Category = film.Category.Name,
                FilmTopics = film.FilmTopics,
                Episodes = film.Episodes,
                Seasons = film.Seasons,
                CreatedDate = film.CreatedDate.ToString("dddd, dd MMMM yyyy")
            };
            return movieDetail;
        }

        public async Task DeleteAsync(int id)
        {
            Film dbFilm = await GetByIdAsync(id);

            _context.Films.Remove(dbFilm);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath + "/assets/img/poster/" + dbFilm.Images);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task CreateAsync(MovieCreateVM model)
        {
            List<FilmImage> images = new();

            foreach (var item in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "/assets/img/poster/");

                images.Add(new FilmImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Film film = new()
            {
                Name = model.Name,
                Description = model.Description,
                MinAge = model.MinAge,
                Duration = model.Duration,
                ResolutionId = model.ResolutionId,
                CategoryId = model.CategoryId,
                Images = images
            };

            await _context.AddAsync(film);
            await _context.SaveChangesAsync();

            foreach (var item in model.CheckBoxes)
            {
                if (item.IsChecked)
                {
                    await _filmTopicService.CreateTopicAsync(film, item.Id);
                }
            }
        }

        public async Task EditAsync(int filmId, MovieEditVM model)
        {
            List<FilmImage> images = new List<FilmImage>();

            Film film = await GetByIdAsync(filmId);

            if (model.NewImage != null && model.NewImage.Any())
            {
                foreach (var item in model.NewImage)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "/assets/img/poster/");
                    images.Add(new FilmImage { Image = fileName, FilmId = filmId });
                }
            }

            film.ResolutionId = model.ResolutionId;
            film.CategoryId = model.CategoryId;
            film.Name = model.Name;
            film.Description = model.Description;
            film.MinAge = model.MinAge;
            film.Duration = model.Duration;
            film.Description = model.Description;

            var currentTopics = await _filmTopicService.FindTopicsByConditionAsync(m => m.FilmId == film.Id);

            //var updatedTopicIds = model.CheckBoxes.Where(c => c.IsChecked).Select(c => c.Id);

            var list = model.CheckBoxes.Where(c => c.IsChecked).ToList();

            List<int> updatedTopicIds = new List<int>();

            foreach (var item in list)
            {
                updatedTopicIds.Add(item.Id);
            }

            var topicsToRemove = currentTopics.Where(t => !updatedTopicIds.Contains(t.TopicId));

            var topicsToAdd = updatedTopicIds.Where(id => !currentTopics.Any(t => t.TopicId == id))
                                        .Select(id => new FilmTopic { FilmId = film.Id, TopicId = id });

            //foreach (var idToAdd in updatedTopicIds)
            //{
            //    await _filmTopicService.RemoveTopicFromProductsAsync(film, idToAdd);
            //}

            foreach (var idToAdd in updatedTopicIds)
            {
                await _filmTopicService.IncludeTopicsToProductAsync(film, idToAdd);
            }

            await _context.FilmImages.AddRangeAsync(images);
            _context.Update(film);
            await _context.SaveChangesAsync();
        }

        public async Task<Film> GetWholeByIdAsync(int? id)
        {
           return await _context.Films.FindAsync(id);
        }
    }
}

