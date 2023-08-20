using System;
using Final_Project.Models;

namespace Final_Project.ViewModels
{
	public class HomeVM
	{
        public List<Film> Films { get; set; }
        public List<FilmTopic> FilmTopics { get; set; }
        public List<Resolution> Resolutions { get; set; }
        public List<ServiceOptions> ServiceOptions { get; set; }
        public List<Service> Services { get; set; }
        public List<Streaming> Streamings { get; set; }
        public List<Topic> Topics { get; set; }
        public List<VideoQuality> VideoQualities { get; set; }
        public List<Category> Categories { get; set; }
    }
}
