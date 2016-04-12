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

namespace ACTS.UI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		static private ILogger _logger;
		static MvcApplication()
		{
			IKernel kernel = new StandardKernel();
			_logger = kernel.Get<ILoggerFactory>().GetCurrentClassLogger();
		}

		public MvcApplication()
			:base()
		{
			_logger.Debug("MVC Application initialized.");
		}

		protected void Application_Start()
		{
			_logger.Trace("MVC Application start configuring.");

			AreaRegistration.RegisterAllAreas();
			AdapterConfig.RegisterAdapters();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

			EmailBodyServiceFactory.DefaultPathToTemplates = Server.MapPath(ConfigurationManager.AppSettings["EmailTemplatesFolder"]);
			LogService.DefaultPathToLogs = Server.MapPath(ConfigurationManager.AppSettings["LogsFolder"]);

			_logger.Trace("MVC Application end configuring.");
		}

		protected void Application_Error()
		{
			Exception lastException = Server.GetLastError();
			_logger.ErrorException(lastException.Message, lastException);
		}
	}
}
