using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	public class EventController : BaseController
	{
		private IEventRepository _repository;
		private readonly ILogger _logger;

		public EventController(IEventRepository eventRepository, ILoggerFactory loggerFactory)
		{
			_repository = eventRepository;
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public ActionResult Table()
		{
			IEnumerable<Event> @event = _repository.Events.OrderBy(e => e.EventId);
			return View("TableEvent", @event);
		}

		public ActionResult Edit(int eventId)
		{
			Event @event = _repository.GetEventById(eventId);
			@event.StartView = @event.StartView?.ToLocalTime();
			@event.EndView = @event.EndView?.ToLocalTime();
			return View("EditEvent", @event);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Event @event, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				@event.UpdateFileForContainer(image);

				@event.StartView = @event.StartView?.ToUniversalTime();
				@event.EndView = @event.EndView?.ToUniversalTime();
				_repository.UpdateEvent(@event);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EventSavedMsg, @event.Title));
				_logger.Info("Event \"{0}\" saved by {1}.", @event.Title, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			}
			else
			{
				// there is something wrong with the data values         
				return View("EditEvent", @event);
			}
		}

		public ActionResult Create()
		{
			var now = DateTime.Now;
			var @event = new Event {
				StartView = now,
				EndView = now.AddDays(7d)
			};
			return View("CreateEvent", @event);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Event @event, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				@event.CreateFileForContainer(image);

				@event.StartView = @event.StartView?.ToUniversalTime();
				@event.EndView = @event.EndView?.ToUniversalTime();
				_repository.CreateEvent(@event);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EventCreatedMsg, @event.Title));
				_logger.Info("Event \"{0}\" created by {1}.", @event.Title, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			}
			else
			{
				// there is something wrong with the data values         
				return View("EditEvent");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int eventId)
		{
			Event deletedEvent = _repository.DeleteEvent(eventId);
			if (deletedEvent != null)
			{
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EventDeletedMsg, deletedEvent.Title));
				_logger.Info("Event \"{0}\" deleted by {1}.", deletedEvent.Title, User.Identity.Name);
			}
			return RedirectToAction(nameof(Table));
		}

		private bool disposedValue = false; // To detect redundant calls

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_repository.Dispose();
					base.Dispose(disposing);
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
	}
}