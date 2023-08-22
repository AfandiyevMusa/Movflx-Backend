using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Pricing;
using Final_Project.Data;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class PricingController : Controller
    {
        //private readonly ILayoutService _layoutService;
        private readonly IVideoService _videoService;
        private readonly IResolutionService _resolutionService;
        private readonly IWebHostEnvironment _env;
        private readonly IPricingService _pricingService;
        private readonly AppDbContext _context;

        public PricingController(IVideoService videoService,
                                 IWebHostEnvironment env,
                                 //ILayoutService layoutService,
                                 IPricingService pricingService,
                                 IResolutionService resolutionService,
                                 AppDbContext context)
        {
            //_layoutService = layoutService;
            _videoService = videoService;
            _resolutionService = resolutionService;
            _env = env;
            _context = context;
            _pricingService = pricingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PricingVM> pricingList = new();

            //IEnumerable<Pricing> pricings = await _context.Pricings.Include(m => m.VideoQuality).Include(m => m.Resolution).Where(m=>!m.SoftDelete).ToListAsync();
            IEnumerable<Pricing> pricings = await _pricingService.GetAllPricingsAsync();

            foreach (Pricing eachPricing in pricings)
            {
                PricingVM model = new()
                {
                    Id = eachPricing.Id,
                    Name = eachPricing.Name,
                    Price = eachPricing.Price,
                    DatePlan = eachPricing.DatePlan,
                    MultiplyWatching = eachPricing.MultiplyWatching,
                    Resolution = eachPricing.Resolution.ResolutionP,
                    VideoQuality = eachPricing.VideoQuality.Quality
                };
                pricingList.Add(model);
            }

            return View(pricingList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Pricing pricing = await _pricingService.GetByIdAsync(id);
            if (pricing is null) return NotFound();
            return View(_pricingService.GetMappedData(pricing));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllModelsRelatedID();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(PricingCreateVM request)
        {
            await GetAllModelsRelatedID();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _pricingService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _pricingService.DeleteAsync((int)id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Pricing pricing = await _pricingService.GetByIdAsync(id);
            if (pricing is null) return NotFound();

            await GetAllModelsRelatedID();

            PricingEditVM model = new()
            {
                Name = pricing.Name,
                DatePlan = pricing.DatePlan,
                MultiplyWatching = pricing.MultiplyWatching,
                Price = pricing.Price,
                ResolutionId = pricing.ResolutionId,
                VideoQualityId = pricing.VideoQualityId
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PricingEditVM request, int? id)
        {
            if (id is null) return BadRequest();
            await GetAllModelsRelatedID();
            Pricing pricing = await _pricingService.GetByIdAsync(id);
            if (pricing is null) return NotFound();

            await _pricingService.EditAsync(pricing.Id, request);

            return RedirectToAction(nameof(Index));
        }

        private async Task GetAllModelsRelatedID()
        {
            ViewBag.videoqualities = await GetVideoQualities();
            ViewBag.resolutions = await GetResolutions();
        }

        private async Task<SelectList> GetVideoQualities()
        {
            List<VideoQuality> videoQualities = await _videoService.GetAllVideoQualities();
            return new SelectList(videoQualities, "Id", "Quality");
        }

        private async Task<SelectList> GetResolutions()
        {
            List<Resolution> resolutions = await _resolutionService.GetAllResolutions();
            return new SelectList(resolutions, "Id", "ResolutionP");
        }
    }
}

