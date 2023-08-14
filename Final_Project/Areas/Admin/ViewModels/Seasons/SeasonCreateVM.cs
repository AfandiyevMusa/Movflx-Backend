using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Seasons
{
	public class SeasonCreateVM
	{
        [Required]
        public string Name { get; set; }
        public int FilmId { get; set; }
    }
}

