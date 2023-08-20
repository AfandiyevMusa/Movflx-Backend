using System;
using Final_Project.Areas.Admin.ViewModels.Pricing;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IPricingService
	{
        Task<List<Pricing>> GetAllPricingsAsync();
        Task<Pricing> GetByIdAsync(int? id);

        Task<Pricing> GetWholeByIdAsync(int? id);
        PricingDetailVM GetMappedData(Pricing pricing);
        Task DeleteAsync(int id);
        Task CreateAsync(PricingCreateVM model);
        Task EditAsync(int pricingId, PricingEditVM model);
        Task<Pricing> GetByIdWithAllIncludesAsync(int? id);
    }
}

