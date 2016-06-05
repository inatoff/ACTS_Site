using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using Ninject.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class TeacherController : BaseController
	{
		private ITeacherRepository _repository;
		private readonly ILogger _logger;

		public TeacherController(ITeacherRepository employeeRepository, ILoggerFactory loggerFactory)
		{
			_repository = employeeRepository;
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public ActionResult Table()
		{
			IEnumerable<Teacher> teachers = _repository.Teachers.Include(t => t.User).OrderBy(t => t.TeacherId);
			return View("TableTeacher", teachers);
		}

		public ActionResult Edit(int teacherId)
		{
			Teacher teacher = _repository.GetTeacherByIdWithLoadedProp(teacherId, t => t.Disciplines,
																				  t => t.Projects,
																				  t => t.Publications,
																				  t => t.ScienceInterests);
			return View("EditTeacher", teacher);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Teacher teacher, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				teacher.UpdateFileForContainer(image);

				_repository.UpdateTeacher(teacher);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.TeacherSavedMsg, teacher.FullName));
				_logger.Info("Teacher \"{0}\" saved by {1}.", teacher.FullName, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			}
			else
				// there is something wrong with the data values         
				return View("EditTeacher", teacher);
		}

		public ActionResult Create()
		{
			return View("CreateTeacher");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Teacher teacher, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				teacher.CreateFileForContainer(image);

				_repository.CreateTeacher(teacher);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.TeacherCreatedMsg, teacher.FullName));
				_logger.Info("Teacher \"{0}\" created by {1}.", teacher.FullName, User.Identity.Name);

				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("CreateTeacher", teacher);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int teacherId)
		{
			Teacher deletedTeacher = _repository.DeleteTeacher(teacherId);
			if (deletedTeacher != null)
			{
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.TeacherDeletedMsg, deletedTeacher.FullName));
				_logger.Info("Teacher \"{0}\" deleted by {1}.", deletedTeacher.FullName, User.Identity.Name);
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