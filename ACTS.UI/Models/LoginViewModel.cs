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
	public class LoginViewModel
	{
		[CustomRequired]
		[Display(Name = nameof(DisplayRes.EmailOrUserNameName), ResourceType = typeof(DisplayRes))]
		public string EmailOrUserName { get; set; }

		[CustomRequired]
		[DataType(DataType.Password)]
		[Display(Name = nameof(GlobalRes.Password), ResourceType = typeof(GlobalRes))]
		public string Password { get; set; }

		[Display(Name = nameof(DisplayRes.RememberMeName), ResourceType = typeof(DisplayRes))]
		public bool RememberMe { get; set; }
	}
}
