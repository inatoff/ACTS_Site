using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class EventController : BaseController
	{
		private IEventRepository _repository;

		public EventController(IEventRepository repository)
		{
			_repository = repository;
		}

		public FileContentResult GetImage(int eventId)
		{
			Event @event = _repository.GetEventById(eventId);
			if (@event != null)
			{
				return File(@event.ImageData, @event.ImageMimeType);
			}
			else
			{
				return null;
			}
		}
	}
}