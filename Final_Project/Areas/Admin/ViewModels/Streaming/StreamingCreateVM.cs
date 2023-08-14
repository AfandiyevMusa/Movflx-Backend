using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Streaming
{
    public class StreamingCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Resolution { get; set; }
    }
}

