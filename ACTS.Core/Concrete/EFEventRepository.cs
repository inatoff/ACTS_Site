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
			Event dbEntry = _context.Events.Find(@event.EventId);
			if (dbEntry != null)
			{
				dbEntry.Modified = DateTime.UtcNow;
				dbEntry.Title = @event.Title;
				dbEntry.UrlSlug = @event.UrlSlug;
				dbEntry.StartView = @event.StartView;
				dbEntry.EndView = @event.EndView;
				dbEntry.Content = @event.Content;
				dbEntry.ImageData = @event.ImageData;
				dbEntry.ImageMimeType = @event.ImageMimeType;

				_context.SaveChanges();
			}
		}
	}
}
