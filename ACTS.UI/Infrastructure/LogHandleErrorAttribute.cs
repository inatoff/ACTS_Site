using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Infrastructure
{
	public class LogHandleErrorAttribute : HandleErrorAttribute
	{
		private readonly ILogger _logger;

		public LogHandleErrorAttribute(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public override void OnException(ExceptionContext filterContext)
		{
			_logger.ErrorException(filterContext.Exception.Message, filterContext.Exception);
			base.OnException(filterContext);
		}
	}
}
