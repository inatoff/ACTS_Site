using ACTS.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.UI
{
	public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute(
			//	 name: "Default",
			//	 url: "{culture}/{controller}/{action}/{id}",
			//	 defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Home", action = "Index", id = UrlParameter.Optional }
			// );

			//routes.MapRoute(
			//	name: "Lang",
			//	url: "{lang}/{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	constraints: new { lang = @"uk|ru|en" }
			//);

			routes.MapMvcAttributeRoutes();


			routes.MapRoute(
				name: "TeachingStaff",
				url: "teachers",
				defaults: new { controller = "TeachingStaff", action = "TeachingStaff", id = UrlParameter.Optional },
				namespaces: new[] { "ACTS.UI.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Department", action = "About", id = UrlParameter.Optional},
				namespaces: new[] { "ACTS.UI.Controllers" }
			);
		}
	}
}
