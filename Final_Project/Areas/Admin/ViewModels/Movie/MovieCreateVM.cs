using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Movie
{
	public class MovieCreateVM
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

        [Required]
        public List<IFormFile> Images { get; set; }
        public List<CheckBoxVM> CheckBoxes { get; set; }
    }
}

