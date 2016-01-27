using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;

namespace ACTS.UI
{
	public class MvcApplication : System.Web.HttpApplication
	{

		protected void Application_Start()
		{
			DataAnnotationsModelValidatorProvider.RegisterAdapter(
				typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(
				typeof(AssertThatAttribute), typeof(AssertThatValidator));
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
		}
	}
}
