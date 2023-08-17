using System;
namespace Final_Project.Areas.Admin.ViewModels.Movie
{
	public class MovieVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int Duration { get; set; }
        public string Resolution { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int Season { get; set; }
    }
}

