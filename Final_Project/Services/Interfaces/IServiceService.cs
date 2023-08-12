using System;
using Final_Project.Models;
using Final_Project.Areas.Admin.ViewModels.Service;

namespace Final_Project.Services.Interfaces
{
	public interface IServiceService
	{
        Task<List<Service>> GetAllServices();
        Task<Service> GetByIdAsync(int? id);
        ServiceDetailVM GetMappedDatasAsync(Service dbService);
        Task DeleteAsync(int id);
    }
}

