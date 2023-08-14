using System;
using Final_Project.Areas.Admin.ViewModels.Streaming;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IStreamingService
	{
        Task<List<Streaming>> GetAllStreams();
        Task<Streaming> GetByIdAsync(int? id);
        Task<IEnumerable<StreamingVM>> GetAllMappedDatasAsync();
        StreamingDetailVM GetMappedDatasAsync(Streaming dbStreaming);
        Task DeleteAsync(int id);
    }
}

