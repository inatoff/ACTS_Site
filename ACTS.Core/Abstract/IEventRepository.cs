using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface IEventRepository
	{
		IQueryable<Event> Events { get; }

		Event GetEventById(int eventId);
		void UpdateEvent(Event @event);
		Event DeleteEvent(int eventId);
		void CreateEvent(Event @event);
	}
}
