using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class NewsController : BaseController
	{
		private INewsRepository repository;

		public NewsController(INewsRepository newsRepository)
		{
			repository = newsRepository;
		}

		/// <summary>
		/// Сторінка з усіма новинами
		/// </summary>
		/// <returns></returns>
		public ViewResult ArchiveUncos()
		{
			IEnumerable<News> uncos = repository.Uncos;
			return View(uncos);
		}

		/// <summary>
		/// Сторінка з новиною
		/// </summary>
		/// <returns></returns>
		public ViewResult PageNews(int newsID)
		{
			News news = repository.Uncos.FirstOrDefault(n => n.NewsID == newsID);
			return View(news);
		}

		public PartialViewResult FooterStringUncos()
		{
			IEnumerable<News> last3uncos = repository.Uncos.OrderBy(n => n.Create).Take(3);
			return PartialView(last3uncos);
		}

		public PartialViewResult NavLastUncos()
		{
			IEnumerable<News> last3uncos = repository.Uncos.OrderBy(n => n.Create).Take(3);
			return PartialView(last3uncos);
		}

		public FileContentResult GetImage(int newsID)
		{
			News news = repository.Uncos.FirstOrDefault(p => p.NewsID == newsID);
			if (news != null)
			{
				return File(news.ImageData, news.ImageMimeType);
			} else
			{
				return null;
			}
		}

		public PartialViewResult GetContent(int newsID)
		{
			News news = repository.Uncos.FirstOrDefault(p => p.NewsID == newsID);
			if (news != null)
			{
				return PartialView("_NewsContent", news);
			} else
			{
				return null;
			}
		}

		//public string GetContent(int newsID)
		//{
		//	News news = repository.Uncos.FirstOrDefault(p => p.NewsID == newsID);
		//	if (news != null)
		//	{
		//		return new HtmlString(news.Content).ToHtmlString();
		//	} else
		//	{
		//		return null;
		//	}
	}
}