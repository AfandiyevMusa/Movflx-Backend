using System;
namespace Final_Project.Models
{
	public class Season:BaseEntity
	{
        public string Name { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}

