using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Resolution
{
	public class ResolutionCreateVM
	{
        [Required]
        public string ResolutionP { get; set; }
    }
}

