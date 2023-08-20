using System;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}

