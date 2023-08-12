using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.Areas.Admin.ViewModels.ServiceOption;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class ServiceOptionService:IServiceOptionService
	{
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceOptionService(AppDbContext context,
                            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<ServiceOptions>> GetAllCities() => await _context.ServiceOptions.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<List<ServiceOptions>> GetAll() => await _context.ServiceOptions.ToListAsync();

        public async Task<ServiceOptions> GetByIdAsync(int? id) => await _context.ServiceOptions.FirstOrDefaultAsync(m => m.Id == id);

        public ServiceOptionDetailVM GetMappedDatasAsync(ServiceOptions dbServiceOptions)
        {
            ServiceOptionDetailVM model = new()
            {
                Title = dbServiceOptions.Title,
                Description = dbServiceOptions.Description,
                CreateDate = dbServiceOptions.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            ServiceOptions dbServiceOptions = await GetByIdAsync(id);

            _context.ServiceOptions.Remove(dbServiceOptions);

            await _context.SaveChangesAsync();
        }
    }
}

