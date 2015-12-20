using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	public class TeacherController : Controller
	{
		private ITeacherRepository repository;

		public TeacherController(ITeacherRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public ActionResult Table()
		{
			IEnumerable<Teacher> teachers = repository.Teachers;
			return View("TableTeacher",teachers);
		}

		public ActionResult Edit(int teacherId)
		{
			Teacher teacher = repository.Teachers.FirstOrDefault(p => p.TeacherID == teacherId);
			return View("EditTeacher", teacher);
		}

		[HttpPost]
		public ActionResult Edit(Teacher teacher, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					teacher.PhotoMimeType = image.ContentType;
					teacher.Photo = new byte[image.ContentLength];
					image.InputStream.Read(teacher.Photo, 0, image.ContentLength);
				}
				repository.SaveTeacher(teacher);
				TempData["infoMessage"] = string.Format("{0} has been saved.", teacher.FullName);
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditTeacher", teacher);
			}
		}

		public ActionResult Create()
		{
			ViewBag.CurrentTreeView = "Create";
			return View("EditTeacher", new Teacher());
		}

		[HttpPost]
		public ActionResult Delete(int teacherId)
		{
			Teacher deletedTeacher = repository.DeleteTeacher(teacherId);
			if (deletedTeacher != null)
			{
				TempData["infoMessage"] = string.Format("{0} was deleted.", deletedTeacher.FullName);
			}
			return RedirectToAction(nameof(Table));
		}
	}
}