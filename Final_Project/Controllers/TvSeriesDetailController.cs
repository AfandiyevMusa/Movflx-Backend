using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Final_Project.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class TvSeriesDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFilmService _filmService;
        private readonly ICategoryServices _categoryServices;
        private readonly ISeasonService _seasonService;
        private readonly IEpisodeService _episodeService;

        public TvSeriesDetailController(AppDbContext context,
                              IFilmService filmService,
                              ICategoryServices categoryServices,
                              ISeasonService seasonService,
                              IEpisodeService episodeService)
        {
            _context = context;
            _filmService = filmService;
            _categoryServices = categoryServices;
            _seasonService = seasonService;
            _episodeService = episodeService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            Film films = await _filmService.GetByIdAsync(id);
            //List<Category> categories = await _categoryServices.GetAllCategories();
            List<Season> seasons = await _seasonService.GetAllByIdAsync(id);
            //List<Episode> episodes = await _episodeService.GetAllByIdAsync(id);

            TvSeriesDetailVM tvSeriesDetailVM = new()
            {
                Films = films,
                //Categories = categories,
                Seasons = seasons,
                //Episodes = episodes
            };

            return View(tvSeriesDetailVM);
        }
    }
}

