using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Blog
{
	public class BlogCreateVM
	{
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int BlogAuthorId { get; set; }
        [Required]
        public List<IFormFile> BlogImages { get; set; }
    }
}

