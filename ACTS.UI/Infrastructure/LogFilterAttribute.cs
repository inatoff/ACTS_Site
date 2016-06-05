using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Infrastructure
{
	class LogFilterAttribute : ActionFilterAttribute
	{
		private ILogger _logger;

		public LogFilterAttribute(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var message = string.Format("Start executing action {1} from {0}Controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
																    filterContext.ActionDescriptor.ActionName);

			_logger.Trace(message);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception != null || filterContext.Canceled)
				return;

			var message = string.Format("Executed action {1} from {0}Controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
																   filterContext.ActionDescriptor.ActionName);

			_logger.Trace(message);
		}
	}
}