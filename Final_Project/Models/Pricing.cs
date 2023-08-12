using System;
namespace Final_Project.Models
{
	public class Pricing:BaseEntity
	{
		public string Name { get; set; }
		public string Price { get; set; }
		public string DatePlan { get; set; }
		public int MultiplyWatching { get; set; }
        public int VideoQualityId { get; set; }
        public VideoQuality VideoQuality { get; set; }
        public int ResolutionId { get; set; }
        public Resolution Resolution { get; set; }
	}
}

