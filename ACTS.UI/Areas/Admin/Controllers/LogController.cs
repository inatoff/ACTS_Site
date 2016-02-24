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

namespace ACTS.UI.Areas.Admin.Controllers
{
	//[Security(Roles = "Admin", RedirectUrl = "Admin/Account/Login")]
	[Authorize(Roles = "Admin")]
	public class LogController : BaseController
	{
		private ILogRepository _repository;
		public LogController(ILogRepository repository)
		{
			_repository = repository;
		}

		[ActionName("Logging")]
		public ActionResult LogsForNDays(double nomDays)
		{
			var logs = _repository.LastLogsForNomDays(nomDays).OrderByDescending(l => l.UtcDate).AsEnumerable();
			return View("Logging", logs); 
		}

		[ActionName("Index")]
		public ActionResult LogsForLastMonth()
		{
			var logs = _repository.LastMonthLogs.OrderByDescending(l => l.UtcDate).AsEnumerable();
			return View("Logging", logs);
		}

		public ActionResult Rss()
		{
			var rssItems = _repository.LastWeekLogs
								  .OrderByDescending(l => l.UtcDate)
								  .AsEnumerable()
								  .Select(log => new SyndicationItem() {
									  Id = log.LogEntryId.ToString(),
									  Content = SyndicationContent.CreatePlaintextContent(log.Message),
									  PublishDate = log.UtcDate.ToLocalTime(),
									  Title = SyndicationContent.CreatePlaintextContent(log.Level.ToString())
								  }).ToList();

			var date = DateTime.Now;

			return new RssResult("Log Reporting",$"Log Provider: Nlog, Log Level: Info, From {date.AddDays(-7d).ToShortDateString()} to {date.ToShortDateString()} (Last Week)", rssItems);
		}
	}
}