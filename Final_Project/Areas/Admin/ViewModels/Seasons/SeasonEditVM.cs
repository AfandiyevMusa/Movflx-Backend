using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Seasons
{
	public class SeasonEditVM
	{
        [Required]
        public string Name { get; set; }
        public int FilmId { get; set; }
        public List<Final_Project.Models.Episode> Episodes { get; set; }
    }
}

