using System;
namespace Final_Project.Models
{
	public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
        public bool IsChecked { get; set; }
    }
}

