using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.ViewModels.Streaming
{
    public class StreamingEditVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
}

