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

        public MovieDetailController(AppDbContext context,
                              IFilmService filmService,
                              ITopicService topicService)
        {
            _context = context;
            _filmService = filmService;
            _topicService = topicService;
        }

        public async Task<IActionResult> Index()
        {
            List<Film> films = await _filmService.GetAllFilms();
            List<Topic> topics = await _topicService.GetAllTopics();

            MovieDetailVM movieDetail = new()
            {
                Films = films,
                Topics = topics
            };

            return View(movieDetail);
        }
    }
}

