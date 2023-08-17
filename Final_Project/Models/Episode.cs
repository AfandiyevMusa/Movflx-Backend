using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
	public class Episode:BaseEntity
	{
		public string Name { get; set; }
        [ForeignKey("Season")]
        public int SeasonId { get; set; }
		public Season Season { get; set; }
        
		public int FilmId { get; set; }
		public Film Film { get; set; }
	}
}

//[ForeignKey("Film")]