using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ACTS.Core.Concrete
{
	public class EFNewsRepository : INewsRepository
	{
		private EFDbContext _context = new EFDbContext();

		public IQueryable<News> Uncos
		{
			get { return _context.Uncos; }
		}

		[Obsolete]
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
			_context.Entry(news).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public void CreateNews(News news)
		{
			news.Created = DateTime.UtcNow;
			_context.Uncos.Add(news);

			_context.SaveChanges();
		}

		public IQueryable<News> GetRandomNewsForLastYear(int count)
		{
			var utcNow = DateTime.UtcNow;
			return _context.Uncos.Where(n => n.Modified.HasValue ? DbFunctions.DiffYears(n.Modified, utcNow) < 1
																 : DbFunctions.DiffYears(n.Created, utcNow) < 1)
								 .OrderBy(o => Guid.NewGuid())
								 .Take(count);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).

				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFNewsRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}