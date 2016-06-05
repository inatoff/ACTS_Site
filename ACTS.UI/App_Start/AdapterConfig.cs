using ACTS.Localization;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.App_Start
{
	public static class AdapterConfig
	{
		public static void RegisterAdapters()
		{
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(AssertThatAttribute), typeof(AssertThatValidator));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CustomRequiredAttribute), typeof(RequiredAttributeAdapter));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CustomMaxLengthAttribute), typeof(MaxLengthAttributeAdapter));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CustomMinLengthAttribute), typeof(MinLengthAttributeAdapter));
		}
	}
}
