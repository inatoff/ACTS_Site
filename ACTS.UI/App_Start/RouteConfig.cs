﻿using ACTS.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.UI
{
	public class RouteConfig
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

			routes.MapRoute(
				name: "Admin",
				url: "Admin/{action}/{id}",
				defaults: new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional}
			);
		}
	}
}
