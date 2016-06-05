using ACTS.UI.Infrastructure;
using Ninject;
using Ninject.Extensions.Logging;
using System.Web.Mvc;

namespace ACTS.UI
{
	public static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			var loggerFactory = DependencyResolver.Current.GetService<ILoggerFactory>();
			filters.Add(new LogFilterAttribute(loggerFactory));
		}
	}
}