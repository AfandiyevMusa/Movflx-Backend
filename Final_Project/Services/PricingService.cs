using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class PricingService: IPricingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PricingService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Pricing>> GetAllPricings() => await _context.Pricings.Include(m=>m.VideoQuality).Include(m=>m.Resolution).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Pricing> GetByIdAsync(int? id) => await _context.Pricings.Include(m => m.Resolution).Include(m => m.VideoQuality).FirstOrDefaultAsync(m => m.Id == id);
    }
}

