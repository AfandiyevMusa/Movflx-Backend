﻿using System;
namespace Final_Project.Models
{
	public class BaseEntity
	{
        public int Id { get; set; }
        public bool SoftDelete { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

