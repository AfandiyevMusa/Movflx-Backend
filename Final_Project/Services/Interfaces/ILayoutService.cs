using System;
using Final_Project.ViewModels;

namespace Final_Project.Services.Interfaces
{
	public interface ILayoutService
	{
        Task<LayoutVM> GetAllDatas();
        Dictionary<string, string> GetAllDictionary();
    }
}

