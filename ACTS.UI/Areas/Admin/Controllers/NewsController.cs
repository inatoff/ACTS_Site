using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.UI.App_LocalResources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
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
		private INewsRepository repository;

		public NewsController(INewsRepository newsRepository)
		{
			repository = newsRepository;
		}

		public ActionResult Table()
		{
			IEnumerable<News> uncos = repository.Uncos.OrderBy(n => n.NewsId);
			return View("TableNews", uncos);
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
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsSavedMsg, news.Title)); 
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditNews", news);
			}
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
				if (image != null)
				{
					news.ImageMimeType = image.ContentType;
					news.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(news.ImageData, 0, image.ContentLength);
				}
				repository.CreateNews(news);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsCreatedMsg, news.Title));
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
			News deletedNews = repository.DeleteNews(newsId);
			if (deletedNews != null)
			{
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.NewsDeletedMsg, deletedNews.Title));
			}
			return RedirectToAction(nameof(Table));
		}
	}
}