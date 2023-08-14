using System;
using Final_Project.Areas.Admin.ViewModels.Seasons;
using Final_Project.Models;
namespace Final_Project.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<List<Season>> GetAllSeasons();
        Task<Season> GetByIdAsync(int? id);
        SeasonDetailVM GetMappedDatasAsync(Season dbSeason);
        Task DeleteAsync(int id);
        Task CreateAsync(SeasonCreateVM model);
        Task<Season> GetWithIncludesAsync(int id);
        Task EditAsync(int seasonId, SeasonEditVM model);
        Task DeleteImageByIdAsync(int id);
    }
}

