using System;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IFilmService
	{
        Task<List<Film>> GetAllFilms();
        Task<Film> GetByIdAsync(int? id);
    }
}

