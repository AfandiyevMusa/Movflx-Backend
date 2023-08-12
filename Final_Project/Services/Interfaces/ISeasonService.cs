using System;
using Final_Project.Models;
namespace Final_Project.Services.Interfaces
{
	public interface ISeasonService
	{
        Task<List<Season>> GetAllSeasons();
        Task<Season> GetByIdAsync(int? id);
    }
}

