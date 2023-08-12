using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.VideoQuality
{
	public class VideoQualityCreateVM
	{
        [Required]
        public string Quality { get; set; }
    }
}

