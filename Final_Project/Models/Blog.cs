using System;
namespace Final_Project.Models
{
	public class Blog:BaseEntity
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public int BlogAuthorId { get; set; }
        public BlogAuthor BlogAuthor { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
        public bool Status { get; set; } = true;
    }
}

