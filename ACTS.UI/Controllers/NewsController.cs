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
			var uncos = repository.Uncos.Where(delegate (News n) {
				if (n.Modified.HasValue)
					return (DateTime.UtcNow - n.Modified.Value).TotalDays < 365;
				return (DateTime.UtcNow - n.Create.Value).TotalDays < 365;
			});
			Random rand = new Random();
			var randomNumbers = uncos.Select(r => rand.Next()).ToArray();
			IEnumerable<News> last3uncos = uncos.Zip(randomNumbers, (n, o) => new { News = n, Order = o })
									 .OrderBy(o => o.Order)
									 .Select(o => o.News)
									 .Take(3);

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
	}
}