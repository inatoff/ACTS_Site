using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface IEventRepository: IDisposable
	{
		IQueryable<Event> Events { get; }

		Event GetEvent(int id);
		void UpdateEvent(Event @event);
		Event DeleteEvent(int id);
		void CreateEvent(Event @event);
	}
}
