using System;
using Final_Project.Models;

namespace Final_Project.Areas.Admin.ViewModels.Movie
{
	public class MovieDetailVM
	{
		public string Name { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int Duration { get; set; }
        public string Resolution { get; set; }
        public string Category { get; set; }
        public ICollection<FilmImage> Images { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public ICollection<FilmTopic> FilmTopics { get; set; }
        public ICollection<Final_Project.Models.Episode> Episodes { get; set; }
        public string CreatedDate { get; set; }
	}
}

