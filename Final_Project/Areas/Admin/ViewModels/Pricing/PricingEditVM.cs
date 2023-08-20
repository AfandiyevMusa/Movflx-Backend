using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Pricing
{
	public class PricingEditVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string DatePlan { get; set; }
        [Required]
        public int MultiplyWatching { get; set; }
        public int VideoQualityId { get; set; }
        public int ResolutionId { get; set; }
    }
}

