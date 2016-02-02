using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ChangeUserNameViewModel
	{
		[Required]
		[Display(Name = "User name*", Description = "You can change your username, which impacts how you sign in.")]
		[AssertThat("UserName != CurrentUserName", ErrorMessage = "User name not changed.")]
		[MinLength(5)]
		public string UserName { get; set; }
		public string CurrentUserName { get; set; }
	}
}