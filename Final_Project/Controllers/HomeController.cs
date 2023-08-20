using System.Diagnostics;
using Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Services.Interfaces;
using Final_Project.Models;
using Final_Project.ViewModels;

namespace Final_Project.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _accessor;
    private readonly IFilmService _filmService;
    private readonly IFilmTopicService _filmTopicService;
    private readonly IResolutionService _resolutionService;
    private readonly IServiceOptionService _serviceOptionService;
    private readonly IServiceService _serviceService;
    private readonly IStreamingService _streamingService;
    private readonly ITopicService _topicService;
    private readonly IVideoService _videoService;
    private readonly ICategoryServices _categoryServices;

    public HomeController(AppDbContext context,
                          IHttpContextAccessor accessor,
                          IFilmService filmService,
                          IFilmTopicService filmTopicService,
                          IResolutionService resolutionService,
                          IServiceOptionService serviceOptionService,
                          IServiceService serviceService,
                          IStreamingService streamingService,
                          ITagService tagService,
                          ITopicService topicService,
                          IVideoService videoService,
                          ICategoryServices categoryServices

        )
    {
        _context = context;
        _accessor = accessor;
        _filmService = filmService;
        _filmTopicService = filmTopicService;
        _resolutionService = resolutionService;
        _serviceOptionService = serviceOptionService;
        _serviceService = serviceService;
        _streamingService = streamingService;
        _topicService = topicService;
        _videoService = videoService;
        _categoryServices = categoryServices;
    }

    public async Task<IActionResult> Index()
    {
        List<Film> films = await _filmService.GetAllFilmsAsync();
        List<FilmTopic> filmTopics = await _filmTopicService.GetAllAsync();
        List<Resolution> resolutions = await _resolutionService.GetAllResolutions();
        List<ServiceOptions> serviceOptions = await _serviceOptionService.GetAllOptions();
        List<Service> services = await _serviceService.GetAllServices();
        List<Streaming> streams = await _streamingService.GetAllStreams();
        List<Topic> topics = await _topicService.GetAllTopics();
        List<VideoQuality> videoQualities = await _videoService.GetAllVideoQualities();
        List<Category> categories = await _categoryServices.GetAllCategories();

        HomeVM home = new()
        {
            Films = films,
            FilmTopics = filmTopics,
            Resolutions = resolutions,
            ServiceOptions = serviceOptions,
            Services = services,
            Streamings = streams,
            Topics = topics,
            VideoQualities = videoQualities,
            Categories = categories
        };


        return View();
    }
}

