using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class TeacherController : BaseController
	{
		private ITeacherRepository repository;

		public TeacherController(ITeacherRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public ActionResult Table()
		{
			IEnumerable<Teacher> teachers = repository.Teachers.OrderBy(t => t.TeacherId);
			return View("TableTeacher",teachers);
		}

		public ActionResult Edit(int teacherId)
		{
			Teacher teacher = repository.GetTeacherById(teacherId);
			return View("EditTeacher", teacher);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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
				repository.UpdateTeacher(teacher);
				TempData.AddMessage(MessageType.Success, $"Teacher with name \"{teacher.FullName}\" successfully saved.");
				return RedirectToAction(nameof(Table));
			} else
			{
				// there is something wrong with the data values         
				return View("EditTeacher", teacher);
			}
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
				if (image != null)
				{
					teacher.PhotoMimeType = image.ContentType;
					teacher.Photo = new byte[image.ContentLength];
					image.InputStream.Read(teacher.Photo, 0, image.ContentLength);
				}
				repository.CreateTeacher(teacher);
				TempData.AddMessage(MessageType.Success, $"Teacher with name \"{teacher.FullName}\" successfully created.");
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
			Teacher deletedTeacher = repository.DeleteTeacher(teacherId);
			if (deletedTeacher != null)
			{
				TempData.AddMessage(MessageType.Success, $"Teacher with name \"{deletedTeacher.FullName}\" successfully deleted.");
			}
			return RedirectToAction(nameof(Table));
		}
	}
}