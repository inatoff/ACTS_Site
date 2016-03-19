using ACTS.Localization;
using ACTS.Localization.Resources;
using ACTS.Localization.Resources.GlobalResources; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class ChangePasswordViewModel
	{
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
	}
}
