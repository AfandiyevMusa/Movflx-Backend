using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.ServiceOption
{
	public class ServiceOptionCreateVM
	{
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

