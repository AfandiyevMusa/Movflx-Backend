using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Topic
{
	public class TopicCreateVM
	{
        [Required]
        public string Name { get; set; }
    }
}

