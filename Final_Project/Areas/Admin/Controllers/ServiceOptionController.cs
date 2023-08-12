using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Helpers;
using Microsoft.AspNetCore.Authorization;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.ServiceOption;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceOptionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServiceOptionService _serviceOptionService;
        private readonly IWebHostEnvironment _env;

        public ServiceOptionController(AppDbContext context,
                                  IServiceOptionService serviceOptionService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _serviceOptionService = serviceOptionService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ServiceOptionVM> list = new();

            List<ServiceOptions> datas = await _context.ServiceOptions.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new ServiceOptionVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    CreateDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            ServiceOptions dbServiceOptions = await _serviceOptionService.GetByIdAsync(id);
            if (dbServiceOptions is null) return NotFound();
            return View(_serviceOptionService.GetMappedDatasAsync(dbServiceOptions));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ServiceOptionCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ServiceOptions serviceOptions = new()
            {
                Title = request.Title,
                Description = request.Description
            };

            await _context.ServiceOptions.AddAsync(serviceOptions);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            ServiceOptions dbServiceOptions = await _serviceOptionService.GetByIdAsync(id);
            if (dbServiceOptions is null) return NotFound();

            return View(new ServiceOptionEditVM { Id = dbServiceOptions.Id, Title = dbServiceOptions.Title, Description = dbServiceOptions.Description });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceOptionEditVM request)
        {
            if (id is null) return BadRequest();
            ServiceOptions dbServiceOptions = await _serviceOptionService.GetByIdAsync(id);
            if (dbServiceOptions is null) return NotFound();

            dbServiceOptions.Title = request.Title;
            dbServiceOptions.Description = request.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _serviceOptionService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

