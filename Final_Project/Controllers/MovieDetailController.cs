using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class MovieDetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFilmService _filmService;
        private readonly ITopicService _topicService;
        private readonly ICategoryServices _categoryServices;

        public MovieDetailController(AppDbContext context,
                              IFilmService filmService,
                              ITopicService topicService,
                              ICategoryServices categoryServices)
        {
            _context = context;
            _filmService = filmService;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index(int? id)
        {
            List<Film> films = await _filmService.GetAllFilmsAsync();
            Film film = await _filmService.GetByIdAsync(id);
            List<Category> categories = await _categoryServices.GetAllByIdAsync(film.CategoryId);

            MovieDetailVM movieDetail = new()
            {
                Films = films,
                Film = film,
                Categories = categories
            };

            return View(movieDetail);
        }
    }
}

