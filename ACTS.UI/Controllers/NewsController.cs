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
		public ActionResult ArchiveUncos()
		{
			IEnumerable<News> uncos = repository.Uncos;
			return View(uncos);
		}

		/// <summary>
		/// Сторінка з новиною
		/// </summary>
		/// <returns></returns>
		public ActionResult PageNews(int newsID)
		{
			News news = repository.Uncos.FirstOrDefault(n => n.NewsID == newsID);
			return View(news);
		}

		public ActionResult FooterStringUncos()
		{
			IEnumerable<News> last3uncos = repository.Uncos.OrderBy(n => n.DataCreate).Take(3);
			return PartialView(last3uncos);
		}

		public ActionResult NavLastUncos()
		{
			IEnumerable<News> last3uncos = repository.Uncos.OrderBy(n => n.DataCreate).Take(3);
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
	}
}