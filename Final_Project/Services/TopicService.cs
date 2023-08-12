using System;
using Final_Project.Areas.Admin.ViewModels.Topic;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class TopicService: ITopicService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TopicService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Topic>> GetAllTopics() => await _context.Topics.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Topic> GetByIdAsync(int? id) => await _context.Topics.FirstOrDefaultAsync(m => m.Id == id);

        public TopicDetailVM GetMappedDatasAsync(Topic dbTopic)
        {
            TopicDetailVM model = new()
            {
                Name = dbTopic.Name,
                CreateDate = dbTopic.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Topic dbTopic = await GetByIdAsync(id);
            _context.Topics.Remove(dbTopic);

            await _context.SaveChangesAsync();
        }
    }
}

