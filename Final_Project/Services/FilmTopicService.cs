using System;
using Final_Project.Data;
using System.Linq.Expressions;
using Final_Project.Services.Interfaces;
using Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class FilmTopicService : IFilmTopicService
    {
        private readonly AppDbContext _context;

        public FilmTopicService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTopicAsync(Film film, int topicId)
        {
            FilmTopic filmTopic = new FilmTopic
            {
                FilmId = film.Id,
                TopicId = topicId
            };

            await _context.AddAsync(filmTopic);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FilmTopic>> GetAllAsync() => await _context.FilmTopics.ToListAsync();

        public async Task<IEnumerable<FilmTopic>> FindTopicsByConditionAsync(Expression<Func<FilmTopic, bool>> expression)
        {
            return await _context.FilmTopics.Where(expression).ToListAsync();
        }

        public async Task RemoveTopicFromProductsAsync(Film film, int topicId)
        {
            IEnumerable<FilmTopic> filmTopics = await FindTopicsByConditionAsync(m => m.FilmId == film.Id && m.TopicId == topicId);
            foreach (var eachTopic in filmTopics)
            {
                _context.FilmTopics.Remove(eachTopic);
            }
        }

        public async Task IncludeTopicsToProductAsync(Film film, int topicId)
        {
            FilmTopic filmTopic = new FilmTopic
            {
                FilmId = film.Id,
                TopicId = topicId
            };

            await _context.AddAsync(filmTopic);
        }
    }
}