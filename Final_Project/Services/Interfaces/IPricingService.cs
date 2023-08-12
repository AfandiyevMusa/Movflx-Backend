using System;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IPricingService
	{
        Task<List<Pricing>> GetAllPricings();
        Task<Pricing> GetByIdAsync(int? id);
    }
}

