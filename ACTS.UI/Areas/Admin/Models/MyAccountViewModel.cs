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
		#region ChangeUserName

		[Required]
		[Display(Name = "User name*", Description = "You can change your username, which impacts how you sign in.")]
		[AssertThat("UserName != CurrentUserName", ErrorMessage = "User name not changed.")]
		[MinLength(5)]
		public string UserName { get; set; }
		public string CurrentUserName { get; set; } 

		#endregion

		#region ChangeEmail

		[Display(Name = "Email adress*", Description = "Changing your email address is an easy, two-step process. Specify the new email address you want to use, and we will send an email to current address allowing you to complete the update.")]
		[AssertThat("Email != CurrentEmail", ErrorMessage = "Email not changed.")]
		[DataType(DataType.EmailAddress)]
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string CurrentEmail { get; set; }

		#endregion

		#region ChangePassword

		[Required]
		[Display(Name = "Current password*")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[Display(Name = "New password*")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "The {0} have to be {2} characters.", MinimumLength = 8)]
		public string NewPassword { get; set; }

		[Display(Name = "Confirm password*")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } 

		#endregion

		#region DeleteCurrentUser

		[Required]
		[Display(Name = "Your username or email")]
		[AssertThat("EmailOrUserName == CurrentUserName || EmailOrUserName == CurrentEmail")]
		public string EmailOrUserName { get; set; }

		[Required]
		[AssertThat("Trim(ConfirmationPhrase) == 'delete my account'")]
		public string ConfirmationPhrase { get; set; }

		#endregion

		public MyAccountViewModel()
		{
		}

		public MyAccountViewModel(string userName, string email)
		{
			CurrentUserName = userName;
			UserName = userName;
			CurrentEmail = email;
			Email = email;
		}
	}
}
