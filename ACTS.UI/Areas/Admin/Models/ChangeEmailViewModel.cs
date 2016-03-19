using ACTS.Localization;
using ACTS.Localization.Resources;
using ACTS.Localization.Resources.GlobalResources; 
using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ChangeEmailViewModel
	{
		[Display(Name = nameof(DisplayRes.EmailAddressName), Description = nameof(DisplayRes.ChangeEmailDescription), ResourceType = typeof(DisplayRes))]
		[AssertThat("Email != CurrentEmail",
			ErrorMessageResourceName = nameof(GlobalRes.EmailNotChangedErrMsg), ErrorMessageResourceType = typeof(GlobalRes))]
		[EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
		[CustomRequired]
		public string Email { get; set; }
		public string CurrentEmail { get; set; }
	}
}