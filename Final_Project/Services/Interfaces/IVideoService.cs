using System;
using Final_Project.Areas.Admin.ViewModels.VideoQuality;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IVideoService
	{
        Task<List<VideoQuality>> GetAllVideoQualities();
        Task<VideoQuality> GetByIdAsync(int? id);
        VideoQualityDetailVM GetMappedDatasAsync(VideoQuality dbVideoQuality);
        Task DeleteAsync(int id);
    }
}

