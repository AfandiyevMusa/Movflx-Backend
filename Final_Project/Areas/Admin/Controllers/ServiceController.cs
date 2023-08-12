using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Services.Interfaces;
using Final_Project.Areas.Admin.ViewModels.Service;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServiceService _serviceService;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context,
                                  IServiceService serviceService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _serviceService = serviceService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ServiceVM> list = new();

            List<Final_Project.Models.Service> datas = await _context.Services.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new ServiceVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    Image = item.Image,
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
            Service dbService = await _serviceService.GetByIdAsync(id);
            if (dbService is null) return NotFound();
            return View(_serviceService.GetMappedDatasAsync(dbService));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ServiceCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image"))
            {
                ModelState.AddModelError("Image", "Please select only image file");
                return View();
            }

            if (request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size maximum 20KB");
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "_" + request.Image.FileName;

            await request.Image.SaveFileAsync(filename, _env.WebRootPath, "/assets/img/banner/");

            Service service = new()
            {
                Title = request.Title,
                Description = request.Description,
                Image = filename
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Service dbService = await _serviceService.GetByIdAsync(id);
            if (dbService is null) return NotFound();

            return View(new ServiceEditVM { Id = dbService.Id, Image = dbService.Image, Title = dbService.Title, Description = dbService.Description });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceEditVM request)
        {
            if (id is null) return BadRequest();
            Service dbService = await _serviceService.GetByIdAsync(id);
            if (dbService is null) return NotFound();

            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = dbService.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size maximum 200KB");
                request.Image = dbService.Image;
                return View(request);
            }

            string oldPath = Path.Combine(_env.WebRootPath + "/assets/img/banner/" + dbService.Image);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;

            await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "/assets/img/banner/");

            dbService.Image = fileName;
            dbService.Title = request.Title;
            dbService.Description = request.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _serviceService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

