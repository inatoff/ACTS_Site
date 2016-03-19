using ACTS.Localization;
using ACTS.Localization.Resources;
using ACTS.Localization.Resources.GlobalResources; 
using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ChangeUserNameViewModel
	{
		[CustomRequired]
		[Display(Name = nameof(DisplayRes.UserNameName), Description = nameof(DisplayRes.UserNameDescription), 
			ShortName = nameof(DisplayRes.UserNameShortName), ResourceType = typeof(DisplayRes))]
		[AssertThat("UserName != CurrentUserName",
			ErrorMessageResourceName = nameof(GlobalRes.UserNameNotChangedErrMsg), ErrorMessageResourceType = typeof(GlobalRes))]
		[MinLength(5)]
		public string UserName { get; set; }
		public string CurrentUserName { get; set; }
	}
}