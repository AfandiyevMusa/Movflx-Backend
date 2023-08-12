using System;
namespace Final_Project.Models
{
	public class BlogAuthor : BaseEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public bool Status { get; set; } = true;
    }
}

