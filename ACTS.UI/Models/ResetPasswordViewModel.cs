using ACTS.Localization;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Models
{
	public class ResetPasswordViewModel
	{
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

		public string Token { get; set; }
		public int UserId { get; set; }
	}
}
