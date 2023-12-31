﻿using System;
using Final_Project.Models;

namespace Final_Project.ViewModels
{
	public class MovieDetailVM
	{
        public List<Film> Films { get; set; }
        public Film Film { get; set; }
        public List<Category> Categories { get; set; }
        public List<Topic> Topics { get; set; }
    }
}

