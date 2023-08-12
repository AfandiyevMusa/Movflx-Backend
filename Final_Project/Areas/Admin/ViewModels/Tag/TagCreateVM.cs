using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Tag
{
	public class TagCreateVM
	{
        [Required]
        public string Name { get; set; }
    }
}

