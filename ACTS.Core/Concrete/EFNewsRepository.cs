using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class EFNewsRepository : INewsRepository
	{
		private EFDbContext _context = new EFDbContext();

		public IQueryable<News> Uncos
		{
			get { return _context.Uncos; }
		}

		public void SaveNews(News news)
		{
			if (news.NewsId == 0)
			{
				news.Created = DateTime.UtcNow;
				_context.Uncos.Add(news);
			} else
			{
				News dbEntry = _context.Uncos.Find(news.NewsId);
				if (dbEntry != null)
				{
					dbEntry.Title = news.Title;
					dbEntry.Modified = DateTime.UtcNow;
					dbEntry.Content = news.Content;
					dbEntry.ImageData = news.ImageData;
					dbEntry.ImageMimeType = news.ImageMimeType;
				}
			}

			_context.SaveChanges();
		}

		public News DeleteNews(int newsId)
		{
			News dbEntry = _context.Uncos.Find(newsId);
			if (dbEntry != null)
			{
				_context.Uncos.Remove(dbEntry);
				_context.SaveChanges();
			}
			return dbEntry;
		}

		public News GetNewsById(int newsId)
		{
			return _context.Uncos.Find(newsId);
		}

		public void UpdateNews(News news)
		{
			News dbEntry = _context.Uncos.Find(news.NewsId);
			if (dbEntry != null)
			{
				dbEntry.Title = news.Title;
				dbEntry.Modified = DateTime.UtcNow;
				dbEntry.Content = news.Content;
				dbEntry.ImageData = news.ImageData;
				dbEntry.ImageMimeType = news.ImageMimeType;

				_context.SaveChanges();
			}
		}

		public void CreateNews(News news)
		{
			news.Created = DateTime.UtcNow;
			_context.Uncos.Add(news);

			_context.SaveChanges();
		}
	}
}