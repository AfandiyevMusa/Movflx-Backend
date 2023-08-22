using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Seasons;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SeasonController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISeasonService _seasonService;
        private readonly IFilmService _filmService;
        private readonly IWebHostEnvironment _env;

        public SeasonController(AppDbContext context,
                              ISeasonService seasonService,
                              IFilmService filmService,
                              IWebHostEnvironment env)
        {
            _context = context;
            _filmService = filmService;
            _env = env;
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SeasonVM> list = new();

            List<Season> datas = await _context.Seasons.Include(m => m.Episodes).Include(m => m.Film).ThenInclude(m=>m.Images).OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new SeasonVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Film = item.Film.Name,
                    FilmImage = item.Film.Images.FirstOrDefault()?.Image,
                    CreatedDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });;
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Season dbSeason = await _seasonService.GetByIdAsync(id);
            if (dbSeason is null) return NotFound();
            return View(_seasonService.GetMappedDatasAsync(dbSeason));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllSeasons();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SeasonCreateVM request)
        {
            await GetAllSeasons();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _seasonService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var season = await _seasonService.GetByIdAsync(id);
            if (season is null) return NotFound();

            await GetAllSeasons();

            SeasonEditVM model = new()
            {
                Name = season.Name,
                FilmId = season.FilmId,
                Episodes = season.Episodes.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(SeasonEditVM request, int? id)
        {
            if (id is null) return BadRequest();
            await GetAllSeasons();
            var season = await _seasonService.GetByIdAsync(id);
            if (season is null) return NotFound();

            if (ModelState.IsValid)
            {
                request.Episodes = season.Episodes.ToList();
                return View();
            }

            await _seasonService.EditAsync(season.Id, request);

            return RedirectToAction(nameof(Index));
        }

        private async Task GetAllSeasons()
        {
            ViewBag.getSeasons = await GetSeasons();
            ViewBag.getFilms = await GetFilms();
        }

        private async Task GetAllSeasonsAndFilteredFilms()
        {
            ViewBag.getFilteredFilms = await GetFilteredFilms();
        }

        private async Task<SelectList> GetSeasons()
        {
            List<Season> seasons = await _seasonService.GetAllSeasons();
            return new SelectList(seasons, "Id", "Name");
        }

        private async Task<SelectList> GetFilms()
        {
            List<Film> films = await _filmService.GetAllFilmsAsync();
            return new SelectList(films, "Id", "Name");
        }

        private async Task<SelectList> GetFilteredFilms()
        {
            List<Film> films = await _filmService.GetAllFilteredFilmsAsync();
            return new SelectList(films, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _seasonService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

