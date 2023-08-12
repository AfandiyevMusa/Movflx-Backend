using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
	public class Episode:BaseEntity
	{
		public string Name { get; set; }
		public int SeasonId { get; set; }
		public Season Season { get; set; }
        [ForeignKey("Film")]
		public int FilmId { get; set; }
		public Film Film { get; set; }
	}
}