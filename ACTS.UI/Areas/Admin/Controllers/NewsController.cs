using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class NewsController : BaseController
	{
		private INewsRepository _repository;
		private readonly ILogger _logger;

		public NewsController(INewsRepository newsRepository, ILoggerFactory loggerFactory)
		{
			_repository = newsRepository;
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public ActionResult Table()
		{
			IEnumerable<News> uncos = _repository.Uncos.OrderBy(n => n.NewsId);
			return View("TableNews", uncos);
		}

		public ActionResult Edit(int newsId)
		{
			News news = _repository.GetNewsById(newsId);
			return View("EditNews", news);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(News news, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				news.UpdateFileForContainer(image);

				_repository.UpdateNews(news);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsSavedMsg, news.Title));
				_logger.Info("News \"{0}\" saved by {1}.", news.Title, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			}
			else
				// there is something wrong with the data values         
				return View("EditNews", news);
		}

		public ActionResult Create()
		{
			return View("CreateNews");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(News news, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				news.CreateFileForContainer(image);

				_repository.CreateNews(news);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsCreatedMsg, news.Title));
				_logger.Info("News \"{0}\" created by {1}.", news.Title, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			}
			else
			{
				// there is something wrong with the data values         
				return View("EditNews");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int newsId)
		{
			News deletedNews = _repository.DeleteNews(newsId);
			if (deletedNews != null)
			{
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsDeletedMsg, deletedNews.Title));
				_logger.Info("News \"{0}\" deleted by {1}.", deletedNews.Title, User.Identity.Name);
			}
			return RedirectToAction(nameof(Table));
		}

		private bool disposedValue = false; // To detect redundant calls

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_repository.Dispose();
					base.Dispose(disposing);
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
	}
}