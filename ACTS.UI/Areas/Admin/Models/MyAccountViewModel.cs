using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveAnnotations.Attributes;
using ACTS.Localization.Resources;
using ACTS.Localization;
using ACTS.UI.App_LocalResources;

namespace ACTS.UI.Areas.Admin.Models
{
	public class MyAccountViewModel
	{
		#region ChangeUserName

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.UserNameName), Description = nameof(DisplayRes.UserNameDescription),
			ShortName = nameof(DisplayRes.UserNameShortName), ResourceType = typeof(DisplayRes))]
		[AssertThat("UserName != CurrentUserName",
			ErrorMessageResourceName = nameof(GlobalRes.UserNameNotChangedErrMsg), ErrorMessageResourceType = typeof(GlobalRes))]
		[MinLength(5)]
		public string UserName { get; set; }
		public string CurrentUserName { get; set; }

		#endregion

		#region ChangeEmail

		[Display(Name = nameof(DisplayRes.EmailAddressName), Description = nameof(DisplayRes.ChangeEmailDescription), ResourceType = typeof(DisplayRes))]
		[AssertThat("Email != CurrentEmail",
			ErrorMessageResourceName = nameof(GlobalRes.EmailNotChangedErrMsg), ErrorMessageResourceType = typeof(GlobalRes))]
		[EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
		[CustomRequired]
		public string Email { get; set; }
		public string CurrentEmail { get; set; }

		#endregion

		#region ChangePassword

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.OldPasswordName), ResourceType = typeof(DisplayRes))]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.NewPasswordName), ResourceType = typeof(DisplayRes))]
		[DataType(DataType.Password)]
		[CustomMaxLength(100)]
		[CustomMinLength(8)]
		public string NewPassword { get; set; }

		[Display(Name = nameof(DisplayRes.ConfirmPasswordName), ResourceType = typeof(DisplayRes))]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword),
			ErrorMessageResourceName = nameof(CompareRes.ComparePasswordErrMsg), ErrorMessageResourceType = typeof(CompareRes))]
		public string ConfirmPassword { get; set; }

		#endregion

		#region DeleteCurrentUser

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.EmailOrUserNameName), ResourceType = typeof(DisplayRes))]
		[AssertThat("EmailOrUserName == CurrentUserName || EmailOrUserName == CurrentEmail")]
		public string EmailOrUserName { get; set; }

		[CustomRequired]
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
