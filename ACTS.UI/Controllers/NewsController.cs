using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class NewsController : BaseController
	{
		private INewsRepository _repository;

		public NewsController(INewsRepository newsRepository)
		{
			_repository = newsRepository;
		}

		/// <summary>
		/// Сторінка з усіма новинами
		/// </summary>
		/// <returns></returns>
		public ViewResult ArchiveUncos()
		{
			IEnumerable<News> uncos = _repository.Uncos;
			return View(uncos);
		}

		/// <summary>
		/// Сторінка з новиною
		/// </summary>
		/// <returns></returns>
		public ViewResult News(int newsID)
		{
			News news = _repository.GetNews(newsID);
			return View(news);
		}

		public PartialViewResult FooterStringUncos()
		{
			IEnumerable<News> rand3Uncos = _repository.GetRandomNewsForLastYear(3).AsNoTracking().ToList();

			return PartialView(rand3Uncos);
		}

		public PartialViewResult NavLastUncos()
		{
			IEnumerable<News> last3Uncos = _repository.Uncos.OrderBy(n => n.Created).Take(3);
			return PartialView(last3Uncos);
		}

		public PartialViewResult GetContent(int newsId)
		{
			News news = _repository.GetNews(newsId);
			if (news != null)
				return PartialView("_NewsContent", news);
			else
				return null;
		}
	}
}