using System;
namespace Final_Project.Models
{
	public class FilmImage:BaseEntity
	{
        public string Image { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public bool IsMain { get; set; }
    }
}

