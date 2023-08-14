using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Streaming;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StreamingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IStreamingService _streamingService;

        public StreamingController(AppDbContext context, IWebHostEnvironment env, IStreamingService streamingService)
        {
            _context = context;
            _env = env;
            _streamingService = streamingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _streamingService.GetAllMappedDatasAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Streaming dbStreaming = await _streamingService.GetByIdAsync(id);

            if (dbStreaming is null) return NotFound();

            return View(_streamingService.GetMappedDatasAsync(dbStreaming));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(StreamingCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Streaming streaming = new()
            {
                Title = request.Title,
                Description = request.Description,
                Resolution = request.Resolution
            };

            await _context.Streamings.AddAsync(streaming);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Streaming dbStreaming = await _streamingService.GetByIdAsync(id);
            if (dbStreaming is null) return NotFound();

            return View(new StreamingEditVM { Resolution = dbStreaming.Resolution, Description = dbStreaming.Description, Title = dbStreaming.Title });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, StreamingEditVM request)
        {
            if (id is null) return BadRequest();
            Streaming dbStreaming = await _streamingService.GetByIdAsync(id);
            if (dbStreaming is null) return NotFound();

            dbStreaming.Title = request.Title;
            dbStreaming.Description = request.Description;
            dbStreaming.Resolution = request.Resolution;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _streamingService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

