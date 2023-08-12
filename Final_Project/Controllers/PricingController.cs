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
    public class PricingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPricingService _pricingService;

        public PricingController(AppDbContext context,
                              IPricingService pricingService)
        {
            _context = context;
            _pricingService = pricingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Pricing> pricings = await _pricingService.GetAllPricings();

            PricingVM pricing = new()
            {
                Pricings = pricings,
            };

            return View(pricing);
        }
    }
}

