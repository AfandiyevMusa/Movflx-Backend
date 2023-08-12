using System;
namespace Final_Project.Models
{
	public class FilmTopic
	{
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}

