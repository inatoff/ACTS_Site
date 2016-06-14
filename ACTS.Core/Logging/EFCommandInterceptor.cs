using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Logging
{
	public class EFCommandInterceptor : IDbCommandInterceptor
	{
		private static readonly ILogger Logger = DependencyResolver.Current.GetService<ILoggerFactory>().GetCurrentClassLogger();

		public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) { }

		public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			Log(command, interceptionContext);
		}

		public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext) { }

		public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			Log(command, interceptionContext);
		}

		public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) { }

		public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			Log(command, interceptionContext);
		}

		private void Log<TResult>(
			DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			bool hasException = interceptionContext.Exception != null;
			var logMess = $"Is async: {interceptionContext.IsAsync}, Has exception: {hasException}, Command Text: {command.CommandText}";

			if (hasException)
				Logger.ErrorException(logMess, interceptionContext.Exception);
			else
				Logger.Trace(logMess);
		}
	}
}
