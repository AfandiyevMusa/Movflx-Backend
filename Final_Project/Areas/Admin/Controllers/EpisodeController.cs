using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Episode;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class EpisodeController : Controller
    {
        private readonly IEpisodeService _episodeService;
        //private readonly ILayoutService _layoutService;
        private readonly IFilmService _filmService;
        private readonly ISeasonService _seasonService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public EpisodeController(IEpisodeService episodeService,
                                 IFilmService filmService,
                                 ISeasonService seasonService,
                                 IWebHostEnvironment env,
                                 //ILayoutService layoutService,
                                 AppDbContext context)
        {
            _episodeService = episodeService;
            _filmService = filmService;
            _env = env;
            _context = context;
            //_layoutService = layoutService;
            _seasonService = seasonService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EpisodeVM> episodeList = new();

            IEnumerable<Episode> episodes = await _episodeService.GetAllEpisodes();

            foreach (Episode eachEpisode in episodes)
            {
                EpisodeVM model = new()
                {
                    Id = eachEpisode.Id,
                    Name = eachEpisode.Name,
                    Film = eachEpisode.Film.Name,
                    Season = eachEpisode.Season.Name
                };
                episodeList.Add(model);
            }

            return View(episodeList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Episode episode = await _episodeService.GetByIdAsync(id);
            if (episode is null) return NotFound();
            return View(_episodeService.GetMappedData(episode));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllModelsRelatedID();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(EpisodeCreateVM request)
        {
            await GetAllModelsRelatedID();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _episodeService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _episodeService.DeleteAsync((int)id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Episode episode = await _episodeService.GetByIdAsync(id);
            if (episode is null) return NotFound();

            await GetAllModelsRelatedID();

            EpisodeEditVM model = new()
            {
                FilmId = episode.FilmId,
                Name = episode.Name,
                SeasonId = episode.SeasonId
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EpisodeEditVM request, int? id)
        {
            if (id is null) return BadRequest();
            await GetAllModelsRelatedID();
            var episode = await _episodeService.GetByIdAsync(id);
            if (episode is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _episodeService.EditAsync(episode.Id, request);

            return RedirectToAction(nameof(Index));
        }

        private async Task GetAllModelsRelatedID()
        {
            ViewBag.films = await GetFilms();
            ViewBag.seasons = await GetSeasons();
        }

        private async Task<SelectList> GetFilms()
        {
            List<Film> films = await _filmService.GetAllFilmsAsync();
            return new SelectList(films, "Id", "Name");
        }

        private async Task<SelectList> GetSeasons()
        {
            List<Season> seasons = await _seasonService.GetAllSeasons();
            return new SelectList(seasons, "Id", "Name");
        }

        public async Task<JsonResult> GetSeasonByFilmId(int filmId)
        {
            var season = await _seasonService.GetAllSeasons();

            return Json(season.Where(m => m.FilmId == filmId).ToList());
        }
    }
}