using System;
using Final_Project.Areas.Admin.ViewModels.Tag;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface ITagService
	{
        Task<List<Tag>> GetAllTags();
        Task<Tag> GetByIdAsync(int? id);
        TagDetailVM GetMappedDatasAsync(Tag dbTag);
        Task DeleteAsync(int id);
    }
}

