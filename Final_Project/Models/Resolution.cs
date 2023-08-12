using System;
namespace Final_Project.Models
{
	public class Resolution:BaseEntity
	{
		public string ResolutionP { get; set; }
        public ICollection<Pricing> Pricings { get; set; }
		public ICollection<Film> Films { get; set; }
	}
}

