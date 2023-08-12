using System;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.Resolution;
namespace Final_Project.Services.Interfaces
{
	public interface IResolutionService
	{
        Task<List<Resolution>> GetAllResolutions();
        Task<Resolution> GetByIdAsync(int? id);
        ResolutionDetailVM GetMappedDatasAsync(Resolution dbResolution);
        Task DeleteAsync(int id);
    }
}

