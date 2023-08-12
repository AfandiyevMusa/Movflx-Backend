using System;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IStreamingService
	{
        Task<List<Streaming>> GetAllStreams();
        Task<Streaming> GetByIdAsync(int? id);
    }
}

