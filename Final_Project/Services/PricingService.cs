using System;
using Final_Project.Areas.Admin.ViewModels.Pricing;
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

        public async Task<List<Pricing>> GetAllPricingsAsync() => await _context.Pricings.Include(m=>m.VideoQuality).Include(m=>m.Resolution).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Pricing> GetByIdAsync(int? id) => await _context.Pricings.Include(m => m.Resolution).Include(m => m.VideoQuality).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Pricing> GetByIdWithAllIncludesAsync(int? id)
        {
            return await _context.Pricings.Include(m => m.Resolution).
                                           Include(m => m.VideoQuality).
                                           FirstOrDefaultAsync(m => m.Id == id);
        }

        public PricingDetailVM GetMappedData(Pricing pricing)
        {
            PricingDetailVM pricingDetail = new()
            {
                Name = pricing.Name,
                Price = pricing.Price,
                DatePlan = pricing.DatePlan,
                MultiplyWatching = pricing.MultiplyWatching,
                VideoQuality = pricing.VideoQuality.Quality,
                Resolution = pricing.Resolution.ResolutionP,
                CreatedDate = pricing.CreatedDate.ToString("dddd, dd MMMM yyyy")
            };
            return pricingDetail;
        }

        public async Task DeleteAsync(int id)
        {
            Pricing dbPricing = await GetByIdAsync(id);

            _context.Pricings.Remove(dbPricing);

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(PricingCreateVM model)
        {
            Pricing pricing = new()
            {
                Name = model.Name,
                Price = model.Price,
                DatePlan = model.DatePlan,
                MultiplyWatching = model.MultiplyWatching,
                VideoQualityId = model.VideoQualityId,
                ResolutionId = model.ResolutionId
            };

            await _context.AddAsync(pricing);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int pricingId, PricingEditVM model)
        {
            Pricing pricing = await GetByIdAsync(pricingId);

            pricing.ResolutionId = model.ResolutionId;
            pricing.VideoQualityId = model.VideoQualityId;
            pricing.Name = model.Name;
            pricing.Price = model.Price;
            pricing.DatePlan = model.DatePlan;
            pricing.MultiplyWatching = model.MultiplyWatching;

            await _context.SaveChangesAsync();
        }

        public async Task<Pricing> GetWholeByIdAsync(int? id)
        {
            return await _context.Pricings.FindAsync(id);
        }
    }
}

