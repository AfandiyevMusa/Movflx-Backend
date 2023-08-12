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
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFilmService _filmService;
        private readonly ICategoryServices _categoryServices;

        public MovieController(AppDbContext context,
                              IFilmService filmService,
                              ICategoryServices categoryServices)
        {
            _context = context;
            _filmService = filmService;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index()
        {
            List<Film> films = await _filmService.GetAllFilms();
            List<Category> categories = await _categoryServices.GetAllCategories();

            MovieVM movie = new()
            {
                Films = films,
                Categories = categories
            };

            return View(movie);
        }
    }
}

