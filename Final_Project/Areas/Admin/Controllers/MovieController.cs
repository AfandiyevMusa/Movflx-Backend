using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Movie;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class MovieController : Controller
    {
        private readonly IFilmService _filmService;
        //private readonly ILayoutService _layoutService;
        private readonly ICategoryServices _categoryServices;
        private readonly ITopicService _topicService;
        private readonly IResolutionService _resolutionService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public MovieController(IFilmService filmService,
                                 ICategoryServices categoryService,
                                 IWebHostEnvironment env,
                                 ITopicService topicService,
                                 //ILayoutService layoutService,
                                 IResolutionService resolutionService,
                                 AppDbContext context)
        {
            _filmService = filmService;
            _categoryServices = categoryService;
            //_layoutService = layoutService;
            _topicService = topicService;
            _resolutionService = resolutionService;
            _env = env;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MovieVM> movieList = new();

            IEnumerable<Film> films = await _filmService.GetAllFilmsAsync();

            foreach (Film eachFilm in films)
            {
                MovieVM model = new()
                {
                    Id = eachFilm.Id,
                    Name = eachFilm.Name,
                    Description = eachFilm.Description,
                    MinAge = eachFilm.MinAge,
                    Duration = eachFilm.Duration,
                    Resolution = eachFilm.Resolution.ResolutionP,
                    Category = eachFilm.Category.Name,
                    Image = eachFilm.Images.FirstOrDefault()?.Image
                };
                movieList.Add(model);
            }

            return View(movieList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Film film = await _filmService.GetByIdAsync(id);
            if (film is null) return NotFound();
            return View(_filmService.GetMappedData(film));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllModelsRelatedID();

            var topics = await _topicService.GetAllTopics();
            List<CheckBoxVM> checkBoxVMs = new();

            foreach (var topic in topics)
            {
                checkBoxVMs.Add(new CheckBoxVM { Id = topic.Id, LabelName = topic.Name, IsChecked = false });
            }

            MovieCreateVM model = new()
            {
                CheckBoxes = checkBoxVMs
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(MovieCreateVM request)
        {
            await GetAllModelsRelatedID();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image"))
                {
                    ModelState.AddModelError("Image", "Please select only image file.");
                    return View(request);
                }

                if (item.CheckFileSize(20000))
                {
                    ModelState.AddModelError("Image", "Please select under 200KB image");
                    return View(request);
                }
            }

            var topics = await _topicService.GetAllTopics();

            await _filmService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _filmService.DeleteAsync((int)id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var film = await _filmService.GetByIdAsync(id);
            if (film is null) return NotFound();

            await GetAllModelsRelatedID();

            var topics = await _topicService.GetAllTopics();

            List<CheckBoxVM> checkBoxVMs = new();

            foreach (var topic in topics)
            {
                bool isChecked = false;
                if (topic.FilmTopics != null)
                {
                    foreach (var filmTopic in topic.FilmTopics)
                    {
                        if (filmTopic.FilmId == film.Id)
                        {
                            isChecked = true;
                            break;
                        }
                    }
                }
            checkBoxVMs.Add(new CheckBoxVM { Id = topic.Id, LabelName = topic.Name, IsChecked = isChecked });
            }

            MovieEditVM model = new()
            {
                CategoryId = film.CategoryId,
                Name = film.Name,
                Description = film.Description,
                MinAge = film.MinAge,
                Duration = film.Duration,
                ResolutionId = film.ResolutionId,
                Images = film.Images,
                CheckBoxes = checkBoxVMs.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(MovieEditVM request, int? id)
        {
            if (id is null) return BadRequest();
            await GetAllModelsRelatedID();
            var movie = await _filmService.GetByIdAsync(id);
            if (movie is null) return NotFound();

            if (ModelState.IsValid)
            {
                request.Images = movie.Images.ToList();
                return View();
            }

            if (request.NewImage != null)
            {
                foreach (var item in request.NewImage)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewImages", "Please select only image file");
                        request.Images = movie.Images.ToList();
                        return View();
                    }

                    if (!item.CheckFileSize(200))
                    {
                        ModelState.AddModelError("NewImages", "Image size must be max 200 KB");
                        request.Images = movie.Images.ToList();
                        return View();
                    }
                }
            }

            await _filmService.EditAsync(movie.Id, request);

            return RedirectToAction(nameof(Index));
        }

        private async Task GetAllModelsRelatedID()
        {
            ViewBag.categories = await GetCategories();
            ViewBag.resolutions = await GetResolutions();
            ViewBag.topics = await GetTopics();
        }

        private async Task<SelectList> GetCategories()
        {
            List<Category> categories = await _categoryServices.GetAllCategories();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetTopics()
        {
            List<Topic> topics = await _topicService.GetAllTopics();
            return new SelectList(topics, "Id", "Name");
        }

        private async Task<SelectList> GetResolutions()
        {
            List<Resolution> resolutions = await _resolutionService.GetAllResolutions();
            return new SelectList(resolutions, "Id", "ResolutionP");
        }
    }
}

