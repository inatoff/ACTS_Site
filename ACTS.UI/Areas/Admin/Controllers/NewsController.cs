using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class NewsController : Controller
	{
		private INewsRepository repository;

		public NewsController(INewsRepository newsRepository)
		{
			repository = newsRepository;
		}

		public ActionResult Table()
		{
			IEnumerable<News> uncos = repository.Uncos.OrderBy(n => n.NewsId);
			return View("TableUncos", uncos);
		}

		public ActionResult Edit(int newsId)
		{
			News news = repository.GetNewsById(newsId);
			return View("EditNews", news);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(News news, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					news.ImageMimeType = image.ContentType;
					news.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(news.ImageData, 0, image.ContentLength);
				}
				repository.UpdateNews(news);
				TempData.AddMessage(MessageType.Success, $"News with title \"{news.Title}\" successfully saved.");
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditNews", news);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(News news, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					news.ImageMimeType = image.ContentType;
					news.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(news.ImageData, 0, image.ContentLength);
				}
				repository.CreateNews(news);
				TempData.AddMessage(MessageType.Success, $"News with title \"{news.Title}\" successfully created.");
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditNews");
			}
		}

		public ActionResult Create()
		{
			return View("CreateNews");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int newsId)
		{
			News deletedNews = repository.DeleteNews(newsId);
			if (deletedNews != null)
			{
				TempData.AddMessage(MessageType.Success, $"News with title \"{deletedNews.Title}\" successfully deleted.");
			}
			return RedirectToAction(nameof(Table));
		}
	}
}