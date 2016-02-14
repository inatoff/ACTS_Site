using ACTS.Localization;
using ACTS.Localization.Resources;
using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class DeleteCurrentViewModel
	{
		public string CurrentUserName { get; set; }
		public string CurrentEmail { get; set; }

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.EmailOrUserNameName), ResourceType = typeof(DisplayRes))]
		[AssertThat("EmailOrUserName == CurrentUserName || EmailOrUserName == CurrentEmail")]
		public string EmailOrUserName { get; set; }

		[CustomRequired]
		[AssertThat("Trim(ConfirmationPhrase) == 'delete my account'")]
		public string ConfirmationPhrase { get; set; }
	}
}