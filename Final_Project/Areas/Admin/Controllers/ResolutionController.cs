using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Services;
using Final_Project.Services.Interfaces;
using Final_Project.ViewComponents;
using Final_Project.Areas.Admin.ViewModels.Resolution;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResolutionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IResolutionService _resolutionService;
        private readonly IWebHostEnvironment _env;

        public ResolutionController(AppDbContext context,
                                  IResolutionService resolutionService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _resolutionService = resolutionService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ResolutionVM> list = new();

            List<Resolution> datas = await _context.Resolutions.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new ResolutionVM
                {
                    Id = item.Id,
                    ResolutionP = item.ResolutionP,
                    CreateDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")

                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Resolution resolution = await _resolutionService.GetByIdAsync(id);
            if (resolution is null) return NotFound();
            return View(_resolutionService.GetMappedDatasAsync(resolution));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ResolutionCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Resolution resolution = new()
            {
                ResolutionP = request.ResolutionP
            };

            await _context.Resolutions.AddAsync(resolution);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Resolution dbResolution = await _resolutionService.GetByIdAsync(id);
            if (dbResolution is null) return NotFound();

            return View(new ResolutionEditVM { Id = dbResolution.Id, ResolutionP = dbResolution.ResolutionP });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, ResolutionEditVM request)
        {
            if (id is null) return BadRequest();
            Resolution dbResolution = await _resolutionService.GetByIdAsync(id);
            if (dbResolution is null) return NotFound();

            dbResolution.ResolutionP = request.ResolutionP;
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _resolutionService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

