using System;
using Final_Project.Models;
using Final_Project.ViewModels;
using System.Linq.Expressions;

namespace Final_Project.Services.Interfaces
{
	public interface IFilmTopicService
	{
        Task CreateTopicAsync(Film film, int topicId);
        Task GetAllAsync();
        Task RemoveTopicFromProductsAsync(Film film, int topicId);
        Task<IEnumerable<FilmTopic>> FindTopicsByConditionAsync(Expression<Func<FilmTopic, bool>> expression);
        Task IncludeTopicsToProductAsync(Film film, int topicId);
    }
}

