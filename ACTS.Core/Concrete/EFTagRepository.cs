using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Concrete
{
	public class EFTagRepository : ITagRepository
	{
		private EFDbContext _context = new EFDbContext();

		public IQueryable<Tag> Tags
		{
			get { return _context.Tags; }
		}

		public Tag Get(string tag)
		{
			var dbEntry = _context.Tags.FirstOrDefault(t => t.KeyWord == tag);

			return dbEntry;
		}

		public void Edit(Tag tag)
		{
			_context.Entry(tag).State = System.Data.Entity.EntityState.Modified;

			_context.SaveChanges();
		}

		public void Delete(Tag tag)
		{
			_context.Tags.Remove(tag);

			_context.SaveChanges();
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
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFTagRepository() {
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
