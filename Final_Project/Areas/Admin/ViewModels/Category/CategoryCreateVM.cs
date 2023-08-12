using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Category
{
	public class CategoryCreateVM
	{
        [Required]
        public string Name { get; set; }
    }
}

