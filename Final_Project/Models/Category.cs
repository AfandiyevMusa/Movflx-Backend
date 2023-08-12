using System;
namespace Final_Project.Models
{
	public class Category:BaseEntity
	{
        public string Name { get; set; }
        public ICollection<Film> Films { get; set; }
    }
}

