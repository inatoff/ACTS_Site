using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ACTS.Core.Identity;
using System.Web.Mvc;
using ExpressiveAnnotations.Attributes;
using System.ComponentModel;
using ACTS.Localization.Resources;
using ACTS.Localization;

namespace ACTS.UI.Areas.Admin.Models
{
	public class AccountViewModel
	{
		[CustomRequired]
		[CustomMinLength(5)]
		[Display(Name = nameof(DisplayRes.UserNameName), ShortName = nameof(DisplayRes.UserNameShortName), ResourceType = typeof(DisplayRes))]
		[Remote("doesUserNameExist", "Account", AreaReference.UseRoot, HttpMethod = "POST", 
			ErrorMessageResourceName = nameof(GlobalRes.UserNameExistErrMsg), ErrorMessageResourceType = typeof(GlobalRes))]
		public string UserName { get; set; }

		[CustomRequired]
		[Display(Name = nameof(DisplayRes.PasswordName), ResourceType = typeof(DisplayRes))]
		[CustomMaxLength(100)]
		[CustomMinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = nameof(DisplayRes.ConfirmPasswordName), ResourceType = typeof(DisplayRes))]
		[DataType(DataType.Password)]
		[System.ComponentModel.DataAnnotations.Compare(nameof(Password), 
			ErrorMessageResourceName = nameof(CompareRes.ComparePasswordErrMsg), ErrorMessageResourceType = typeof(CompareRes))]
		public string ConfirmPassword { get; set; }

		[Display(Name = nameof(DisplayRes.EmailAddressName), ResourceType = typeof(DisplayRes))]
		[RequiredIf("SendVerification == true",
			ErrorMessageResourceName = nameof(RequiredRes.RequiredIfSendVerificationErrMsg), ErrorMessageResourceType = typeof(RequiredRes))]
		//[CustomRequired]
		[EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
		public string Email { get; set; }

		[Display(Name = nameof(DisplayRes.SendVerificationName), ResourceType = typeof(DisplayRes))]
		public bool SendVerification { get; set; }

		[Display(Name = nameof(DisplayRes.RolesName), ResourceType = typeof(DisplayRes))]
		public IList<RoleItem> Roles { get; set; }

		public IEnumerable<string> SelectedRoles { get { return Roles.Where(r => r.Selected).Select(r => r.Value); } }

		[Display(Name = nameof(DisplayRes.PairTeacherIdName), ResourceType = typeof(DisplayRes))]
		public int? PairTeacherId { get; set; }
	}
}
