using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.Owin;
using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Identity;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using System.ServiceModel.Syndication;
using ACTS.Core.Logging;
using System.Threading.Tasks;
using ACTS.UI.Services.Logging;

namespace ACTS.UI.Areas.Admin.Controllers
{
	//[Security(Roles = "Admin", RedirectUrl = "Admin/Account/Login")]
	[Authorize(Roles = "Admin")]
	public class LoggingController : BaseController
	{
#if DEBUG
		const LogLevel _defaultLevel = LogLevel.Trace;
#else
		const LogLevel _defaultLevel = LogLevel.Info;
#endif

		public async Task<ActionResult> ByLevelAndDate(DateTime startDate, DateTime endDate, LogLevel logLevel = _defaultLevel)
		{
			var logs = (await LogService.GetByDateAndLevelAsync(startDate.ToUniversalTime(), endDate.ToUniversalTime(), logLevel))
						.OrderByDescending(l => l.UtcDate)
						.AsEnumerable();

			var model = new LogViewModel(logLevel, startDate, endDate, logs);

			return View("Logging", model);
		}

		public async Task<ActionResult> Index()
		{
			var logs = (await LogService.GetLastMonthLogsAsync()).OrderByDescending(l => l.UtcDate).AsEnumerable();
			var model = new LogViewModel(_defaultLevel, DateTime.Now.AddMonths(-1), logs);

			return View("Logging", model);
		}

		public async Task<ActionResult> Rss()
		{
			var rssItems = (await LogService.GetLastWeekLogsAsync())
								  .OrderByDescending(l => l.UtcDate)
								  .AsEnumerable()
								  .Select(log => new SyndicationItem()
								  {
									  Id = log.Id.ToString(),
									  Content = SyndicationContent.CreatePlaintextContent(log.Message),
									  PublishDate = log.UtcDate.ToLocalTime(),
									  Title = SyndicationContent.CreatePlaintextContent(log.Level.ToString())
								  }).AsEnumerable();

			var date = DateTime.Now;

			return new RssResult("Log Reporting", $"Log Provider: Nlog, Log Level: Info, From {date.AddDays(-7d).ToShortDateString()} to {date.ToShortDateString()} (Last Week)", rssItems);
		}
	}
}