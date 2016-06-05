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
using NLog.LayoutRenderers;
using Ninject;
using Ninject.Extensions.Logging;
using NLog.Config;
using ACTS.UI.Services;
using System.Configuration;
using Ninject.Web.Common;

namespace ACTS.UI
{
	public class MvcApplication : HttpApplication
	{
		private static ILogger _logger;

		protected static ILogger Logger
		{
			get
			{
				return _logger ?? (_logger = DependencyResolver.Current.GetService<ILoggerFactory>().GetCurrentClassLogger());
			}
		}

		public MvcApplication()
			:base()
		{
			Logger.Debug("MVC Application initialized.");
		}

		protected void Application_Start()
		{
			Logger.Trace("MVC Application start configuring.");

			AreaRegistration.RegisterAllAreas();
			AdapterConfig.RegisterAdapters();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			Logger.Trace("MVC Application end configuring.");
		}

		protected void Application_Error()
		{
			Exception lastException = Server.GetLastError();
			Logger.ErrorException(lastException.Message, lastException);
		}
	}
}
