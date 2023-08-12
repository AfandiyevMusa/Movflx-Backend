using System;
//using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        //private readonly ILayoutService _layoutService;

        //public HeaderViewComponent(ILayoutService layoutService)
        //{
        //    _layoutService = layoutService;
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var datas = await _layoutService.GetAllDatas();

            return await Task.FromResult(View());
        }
    }
}

