using System;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.Episode;
using Final_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services.Interfaces
{
	public interface IEpisodeService
	{
        Task<List<Episode>> GetAllEpisodes();
        Task<Episode> GetByIdAsync(int? id);

        EpisodeDetailVM GetMappedData(Episode episode);
        Task DeleteAsync(int id);
        Task CreateAsync(EpisodeCreateVM model);
        Task EditAsync(int episodeId, EpisodeEditVM model);
        Task<Episode> GetByIdWithAllIncludesAsync(int? id);
    }
}

