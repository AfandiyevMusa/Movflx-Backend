using System;
using Final_Project.Models;
namespace Final_Project.Areas.Admin.ViewModels.Contact
{
	public class ContactDetailVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ApplicantName { get; set; }
        public EmailModel EmailModel { get; set; }
    }
}