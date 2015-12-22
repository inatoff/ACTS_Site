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
		private EFDbContext context = new EFDbContext();

		public IQueryable<News> Uncos
		{
			get { return context.Uncos; }
		}

		//public void SaveNews(News news)
		//{
		//	if (news.NewsID == 0)
		//	{
		//		news.Create = DateTime.UtcNow;
		//		context.Uncos.Add(news);
		//	} else
		//	{
		//		News dbEntry = context.Uncos.Find(news.NewsID);
		//		if (dbEntry != null)
		//		{
		//			dbEntry.Title = news.Title;
		//			dbEntry.Modified = DateTime.UtcNow;
		//			dbEntry.Content = news.Content;
		//			dbEntry.ImageData = news.ImageData;
		//			dbEntry.ImageMimeType = news.ImageMimeType;
		//		}
		//	}

		//	context.SaveChanges();
		//}

		public News DeleteNews(int newsID)
		{
			News dbEntry = context.Uncos.Find(newsID);
			if (dbEntry != null)
			{
				context.Uncos.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}

		public News GetNewsById(int newsId)
		{
			return Uncos.FirstOrDefault(p => p.NewsID == newsId);
		}

		public void SaveUpdatedNews(News news)
		{
			News dbEntry = context.Uncos.Find(news.NewsID);
			if (dbEntry != null)
			{
				dbEntry.Title = news.Title;
				dbEntry.Modified = DateTime.UtcNow;
				dbEntry.Content = news.Content;
				dbEntry.ImageData = news.ImageData;
				dbEntry.ImageMimeType = news.ImageMimeType;
			}

			context.SaveChanges();

		}

		public void SaveNewNews(News news)
		{
			news.Create = DateTime.UtcNow;
			context.Uncos.Add(news);

			context.SaveChanges();
		}
	}
}