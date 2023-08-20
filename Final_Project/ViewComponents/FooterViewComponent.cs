using System;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents
{
	public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}

