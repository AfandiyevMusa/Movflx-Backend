using System;
namespace Final_Project.Models
{
	public class Film:BaseEntity
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int Duration { get; set; }
        public ICollection<FilmImage> Images { get; set; }
        public int ResolutionId { get; set; }
        public Resolution Resolution { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<FilmTopic> FilmTopics { get; set; }

        public ICollection<Episode> Episodes { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}