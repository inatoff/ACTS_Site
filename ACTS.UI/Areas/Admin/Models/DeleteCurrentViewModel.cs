using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class DeleteCurrentViewModel
	{
		public string CurrentUserName { get; set; }
		public string CurrentEmail { get; set; }

		[Required]
		[Display(Name = "Email or login")]
		[AssertThat("EmailOrUserName == CurrentUserName || EmailOrUserName == CurrentEmail")]
		public string EmailOrUserName { get; set; }

		[Display(Name = "To verify, type <span class=\"confirmation_phrase noselect\"> delete my account</span> below:")]
		[AssertThat("ConfirmationPhrase == 'delete my account'")]
		public string ConfirmationPhrase { get; set; }
	}
}