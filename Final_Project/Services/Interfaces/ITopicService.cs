using System;
using Final_Project.Areas.Admin.ViewModels.Topic;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface ITopicService
	{
        Task<List<Topic>> GetAllTopics();
        Task<Topic> GetByIdAsync(int? id);
        TopicDetailVM GetMappedDatasAsync(Topic dbTopic);
        Task DeleteAsync(int id);
    }
}

