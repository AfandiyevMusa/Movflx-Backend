using System;
using Final_Project.Areas.Admin.ViewModels.Streaming;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class StreamingService : IStreamingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StreamingService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Streaming>> GetAllStreams() => await _context.Streamings.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Streaming> GetByIdAsync(int? id) => await _context.Streamings.FirstOrDefaultAsync(m => m.Id == id);

        public StreamingDetailVM GetMappedDatasAsync(Streaming dbStreaming)
        {
            StreamingDetailVM model = new()
            {
                Title = dbStreaming.Title,
                Resolution = dbStreaming.Resolution,
                Description = dbStreaming.Description,
                CreateDate = dbStreaming.CreatedDate.ToString("dd-MM-yyyy"),
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            Streaming dbStreaming = await GetByIdAsync(id);

            _context.Streamings.Remove(dbStreaming);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StreamingVM>> GetAllMappedDatasAsync()
        {
            List<StreamingVM> sliderList = new();

            IEnumerable<Streaming> streamings = await GetAllStreams();

            foreach (Streaming streaming in streamings)
            {
                StreamingVM model = new()
                {
                    Id = streaming.Id,
                    Title = streaming.Title,
                    Description = streaming.Description,
                    Resolution = streaming.Resolution,
                    CreateDate = streaming.CreatedDate.ToString("dd-MM-yyyy")
                };

                sliderList.Add(model);
            }

            return sliderList;
        }
    }
}

