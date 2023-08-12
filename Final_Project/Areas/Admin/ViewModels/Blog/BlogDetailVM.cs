using System;
namespace Final_Project.Areas.Admin.ViewModels.Blog
{
	public class BlogDetailVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlogAuthor { get; set; }
        public ICollection<Final_Project.Models.BlogImage> BlogImages { get; set; }
        public string CreateDate { get; set; }
    }
}

