using System;
namespace Final_Project.Models
{
	public class Topic:BaseEntity
	{
        public string Name { get; set; }
        public ICollection<FilmTopic> FilmTopics { get; set; }
        public bool IsChecked { get; set; }
    }
}

