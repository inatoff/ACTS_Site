using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Localization
{
	public class CustomRequiredAttribute : RequiredAttribute
	{
		public CustomRequiredAttribute()
			: base()
		{
			ErrorMessageResourceType = typeof(RequiredRes);
			ErrorMessageResourceName = nameof(RequiredRes.RequiredFieldErrMsg);
		}

		public override string FormatErrorMessage(string name)
		{
			var nameWithoutStars = name.TrimEnd(new char[] { '*' });
			return base.FormatErrorMessage(nameWithoutStars);
		}
	}
}
