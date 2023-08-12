using System;
using Final_Project.Areas.Admin.ViewModels.ServiceOption;
using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
	public interface IServiceOptionService
	{
        Task<List<ServiceOptions>> GetAllCities();
        Task<List<ServiceOptions>> GetAll();
        Task<ServiceOptions> GetByIdAsync(int? id);
        ServiceOptionDetailVM GetMappedDatasAsync(ServiceOptions dbServiceOptions);
        Task DeleteAsync(int id);
    }
}

