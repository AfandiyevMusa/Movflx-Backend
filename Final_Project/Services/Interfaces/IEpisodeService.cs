using System;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IEpisodeService
	{
        Task<List<Episode>> GetAllEpisodes();
        Task<Episode> GetByIdAsync(int? id);
    }
}

