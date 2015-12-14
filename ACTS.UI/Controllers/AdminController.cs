using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class AdminController : BaseController
	{
		private INewsRepository _newsRepository;

		public AdminController(INewsRepository newsRepository)
		{
			_newsRepository = newsRepository;
		}

		public ActionResult Dashboard()
		{
			return View();
		}

		public ActionResult ListEmployee()
		{
			return View();
		}

		public ActionResult ListUncos()
		{
			IEnumerable<News> uncos = _newsRepository.Uncos;
			return View(uncos);
		}

		public ActionResult EditNews(int newsId)
		{
			News news = _newsRepository.Uncos.FirstOrDefault(p => p.NewsID == newsId);
			return View(news);
		}

		[HttpPost]
		public ActionResult EditNews(News news, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					news.ImageMimeType = image.ContentType;
					news.ImageData = new byte [image.ContentLength];
					image.InputStream.Read(news.ImageData, 0, image.ContentLength);
				}
				_newsRepository.SaveNews(news);
				TempData ["message"] = string.Format("{0} has been saved", news.Title);
				return RedirectToAction("ListUncos");
			} else
			{
				// there is something wrong with the data values         
				return View(news);
			}
		}

		public ActionResult CreateNews()
		{
			return View("EditNews", new News ());
		}

		[HttpPost]
		public ActionResult DeleteNews(int newsId)
		{
			News deletedNews = _newsRepository.DeleteNews(newsId);
			if (deletedNews != null)
			{
				TempData ["message"] = string.Format("{0} was deleted", deletedNews.Title);
			}
			return RedirectToAction("ListUncos");
		}
	}
}