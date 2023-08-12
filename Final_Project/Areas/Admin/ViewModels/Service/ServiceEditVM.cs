using System;
namespace Final_Project.Areas.Admin.ViewModels.Service
{
	public class ServiceEditVM
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

