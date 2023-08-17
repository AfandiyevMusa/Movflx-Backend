using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Episode
{
	public class EpisodeEditVM
	{
        [Required]
        public string Name { get; set; }

        public int FilmId { get; set; }
        public int SeasonId { get; set; }
    }
}

