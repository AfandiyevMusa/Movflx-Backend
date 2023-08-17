using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Final_Project.Models;

namespace Final_Project.Areas.Admin.ViewModels.Movie
{
	public class MovieEditVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int Duration { get; set; }

        public int ResolutionId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<FilmImage> Images { get; set; }
        public List<IFormFile>? NewImage { get; set; }
        public List<CheckBoxVM> CheckBoxes { get; set; }
    }
}

