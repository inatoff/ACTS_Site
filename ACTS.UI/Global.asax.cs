using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ACTS.UI.App_Start;

namespace ACTS.UI
{
	public class MvcApplication : System.Web.HttpApplication
	{

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			AdapterConfig.RegisterAdapters();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
			EmailBodyFactory.DefaultPathToTemplates = Server.MapPath("~/EmailTemplates/");
		}
	}
}
