using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ProfileViewModel
	{
		public int Id { get; internal set; }

		[Required]
		[Display(Name = "User name*", Description = "You can change your username, which impacts how you sign in.")]
		public string UserName { get; set; }

		[Display(Name = "Email adress*", Description = "Changing your email address is an easy, two-step process. Specify the new email address you want to use, and we will send an email to that address allowing you to complete the update.")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Current password*")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[Display(Name = "New password*")]
		[DataType(DataType.Password)]
		[Compare(nameof(ConfirmPassword), ErrorMessage = "The new password and confirmation password do not match")]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Confirm password*")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
