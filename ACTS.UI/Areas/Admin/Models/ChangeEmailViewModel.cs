using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ChangeEmailViewModel
	{
		[Display(Name = "Email adress*", Description = "Changing your email address is an easy, two-step process. Specify the new email address you want to use, and we will send an email to that address allowing you to complete the update.")]
		[AssertThat("Email != CurrentEmail", ErrorMessage = "Email not changed.")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
		public string CurrentEmail { get; set; }
	}
}