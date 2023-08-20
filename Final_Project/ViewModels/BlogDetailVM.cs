using System;
using Final_Project.Models;

namespace Final_Project.ViewModels
{
	public class BlogDetailVM
	{
        public Blog Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public Category Categories { get; set; }
    }
}

