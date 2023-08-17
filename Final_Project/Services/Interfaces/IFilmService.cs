using System;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.Movie;

namespace Final_Project.Services.Interfaces
{
	public interface IFilmService
	{
        Task<List<Film>> GetAllFilmsAsync();
        Task<Film> GetByIdAsync(int? id);

        Task<Film> GetWholeByIdAsync(int? id);
        MovieDetailVM GetMappedData(Film film);
        Task DeleteAsync(int id);
        Task CreateAsync(MovieCreateVM model);
        Task EditAsync(int productId, MovieEditVM model);
        Task<Film> GetByIdWithAllIncludesAsync(int? id);
    }
}

