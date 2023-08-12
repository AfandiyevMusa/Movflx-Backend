using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Blog
{
	public class BlogEditVM
	{
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int BlogAuthorId { get; set; }
        public List<Final_Project.Models.BlogImage> BlogImages { get; set; }
        public List<IFormFile>? NewBlogImages { get; set; }
    }
}

