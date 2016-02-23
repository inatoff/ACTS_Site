using ACTS.UI.Infrastructure;
using Ninject;
using Ninject.Extensions.Logging;
using System.Web.Mvc;

namespace ACTS.UI
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			var kernel = new StandardKernel();
			ILoggerFactory loggerFactory = kernel.Get<ILoggerFactory>();
			filters.Add(new LogFilterAttribute(loggerFactory));
		}
	}
}