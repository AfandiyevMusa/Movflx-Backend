using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Areas.Admin.ViewModels.VideoQuality;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoQualityController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IVideoService _videoService;
        private readonly IWebHostEnvironment _env;

        public VideoQualityController(AppDbContext context,
                                  IVideoService videoService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _videoService = videoService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VideoQualityVM> list = new();

            List<VideoQuality> datas = await _context.VideoQualities.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new VideoQualityVM
                {
                    Id = item.Id,
                    Quality = item.Quality,
                    CreateDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            VideoQuality videoQuality = await _videoService.GetByIdAsync(id);
            if (videoQuality is null) return NotFound();
            return View(_videoService.GetMappedDatasAsync(videoQuality));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(VideoQualityCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            VideoQuality videoQuality = new()
            {
                Quality = request.Quality
            };

            await _context.VideoQualities.AddAsync(videoQuality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            VideoQuality dbVideoQuality = await _videoService.GetByIdAsync(id);
            if (dbVideoQuality is null) return NotFound();

            return View(new VideoQualityEditVM { Id = dbVideoQuality.Id, Quality = dbVideoQuality.Quality });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, VideoQualityEditVM request)
        {
            if (id is null) return BadRequest();
            VideoQuality dbVideoQuality = await _videoService.GetByIdAsync(id);
            if (dbVideoQuality is null) return NotFound();

            dbVideoQuality.Quality = request.Quality;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _videoService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

