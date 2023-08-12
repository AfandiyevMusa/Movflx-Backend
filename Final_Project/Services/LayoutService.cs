using System;
using Final_Project.Data;
using Final_Project.Models;
using System.Security.Claims;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Final_Project.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext context,
                             IHttpContextAccessor accessor,
                             UserManager<AppUser> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<LayoutVM> GetAllDatas()
        {
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            return new LayoutVM { SettingData = datas, UserName = user.UserName };
        }

        public Dictionary<string, string> GetAllDictionary()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}

