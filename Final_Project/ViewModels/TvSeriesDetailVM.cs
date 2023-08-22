using System;
using Final_Project.Models;

namespace Final_Project.ViewModels
{
	public class TvSeriesDetailVM
	{
        public Film Films { get; set; }
        public List<Category> Categories { get; set; }
        public List<Season> Seasons { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}

