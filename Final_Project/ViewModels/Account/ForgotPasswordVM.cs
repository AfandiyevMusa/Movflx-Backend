using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Account
{
	public class ForgotPasswordVM
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

