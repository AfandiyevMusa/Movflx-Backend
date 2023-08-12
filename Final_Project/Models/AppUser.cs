using System;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg;

namespace Final_Project.Models
{
	public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}

