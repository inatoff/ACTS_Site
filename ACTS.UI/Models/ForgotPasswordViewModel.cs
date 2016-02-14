using ACTS.Localization;
using ACTS.Localization.Resources;
using ACTS.UI.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Models
{
	public class ForgotPasswordViewModel
	{
		[CustomRequired]
		[EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
		[Display(Name = nameof(DisplayRes.EmailName), ResourceType = typeof(DisplayRes))]
		public string Email { get; set; }
	}
}
