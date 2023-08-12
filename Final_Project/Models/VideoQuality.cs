using System;
namespace Final_Project.Models
{
	public class VideoQuality:BaseEntity
	{
		public string Quality { get; set; }
        public ICollection<Pricing> Pricings { get; set; }
    }
}

