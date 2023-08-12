using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.Areas.Admin.ViewModels.Service;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class ServiceService: IServiceService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Service>> GetAllServices() => await _context.Services.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Service> GetByIdAsync(int? id) => await _context.Services.FirstOrDefaultAsync(m => m.Id == id);

        public ServiceDetailVM GetMappedDatasAsync(Service dbService)
        {
            ServiceDetailVM model = new()
            {
                Title = dbService.Title,
                Image = dbService.Image,
                Description = dbService.Description,
                CreateDate = dbService.CreatedDate.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Service dbService = await GetByIdAsync(id);

            _context.Services.Remove(dbService);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath + "/assets/img/banner/" + dbService.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}

