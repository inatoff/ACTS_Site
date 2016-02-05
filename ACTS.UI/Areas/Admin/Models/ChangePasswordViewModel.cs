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
		[Required]
		[Display(Name = "Current password*")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[Display(Name = "New password*")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "The {0} have to be {2} characters.", MinimumLength = 8)]
		public string NewPassword { get; set; }

		[Display(Name = "Confirm password*")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

	}
}
