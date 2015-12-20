using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public partial class NewsController : BaseController
	{
		public ActionResult Table()
		{
			IEnumerable<News> uncos = repository.Uncos;
			return View("TableUncos", uncos);
		}

		public ActionResult Edit(int newsId)
		{
			News news = repository.Uncos.FirstOrDefault(p => p.NewsID == newsId);
			return View("EditNews", news);
		}

		[HttpPost]
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
				repository.SaveNews(news);
				TempData["message"] = string.Format("{0} has been saved", news.Title);
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditNews", news);
			}
		}

		public ActionResult Create()
		{
			ViewBag.CurrentTreeView = "Create";
			return View("EditNews", new News());
		}

		[HttpPost]
		public ActionResult Delete(int newsId)
		{
			News deletedNews = repository.DeleteNews(newsId);
			if (deletedNews != null)
			{
				TempData["message"] = string.Format("{0} was deleted", deletedNews.Title);
			}
			return RedirectToAction(nameof(Table));
		}
	}
}