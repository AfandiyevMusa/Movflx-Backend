using System;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
	public class StreamingService: IStreamingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StreamingService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Streaming>> GetAllStreams() => await _context.Streamings.Include(m=>m.Resolution).Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Streaming> GetByIdAsync(int? id) => await _context.Streamings.Include(m => m.Resolution).FirstOrDefaultAsync(m => m.Id == id);
    }
}

