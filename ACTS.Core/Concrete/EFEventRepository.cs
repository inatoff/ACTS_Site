using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class EFEventRepository : IEventRepository
	{
		private EFDbContext _context = new EFDbContext();

		public IQueryable<Event> Events
		{
			get
			{
				return _context.Events;
			}
		}

		public void CreateEvent(Event @event)
		{
			@event.Created = DateTime.UtcNow;
			_context.Events.Add(@event);
			_context.SaveChanges();
		}

		public Event DeleteEvent(int eventId)
		{
			Event @event = _context.Events.Find(eventId);
			if (@event != null)
			{
				_context.Events.Remove(@event);
				_context.SaveChanges();
			}
			return @event;
		}

		public Event GetEventById(int eventId)
		{
			return _context.Events.Find(eventId);
		}

		public void UpdateEvent(Event @event)
		{
			_context.Entry(@event).State = System.Data.Entity.EntityState.Modified;
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
		// ~EFEventRepository() {
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
