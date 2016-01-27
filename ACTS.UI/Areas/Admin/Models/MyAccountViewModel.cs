using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveAnnotations.Attributes;

namespace ACTS.UI.Areas.Admin.Models
{
	public class MyAccountViewModel
	{
		[Required]
		[Display(Name = "User name*", Description = "You can change your username, which impacts how you sign in.")]
		[AssertThat("UserName != OldUserName", ErrorMessage = "User name not changed.")]
		[MinLength(5)]
		public string UserName { get; set; }
		public string OldUserName { get; set; }

		[Display(Name = "Email adress*", Description = "Changing your email address is an easy, two-step process. Specify the new email address you want to use, and we will send an email to that address allowing you to complete the update.")]
		[AssertThat("Email != OldEmail", ErrorMessage = "Email not changed.")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
		public string OldEmail { get; set; }

		[Required]
		[Display(Name = "Current password*")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[Display(Name = "New password*")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "The {0} have to be {2} characters.", MinimumLength = 8)]
		public string NewPassword { get; set; }

		[Required]
		[Display(Name = "Confirm password*")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		//[AssertThat("ConfirmationPhrase == Phrase", ErrorMessage = "Write 'delete my account'")]
		[Compare(nameof(Phrase), ErrorMessage = "Write 'delete my account'.")]
		public string ConfirmationPhrase { get; set; }

		public string Phrase { get; set; } = "delete my account";
	}
}
