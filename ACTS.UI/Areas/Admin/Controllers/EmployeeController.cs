using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.UI.Helpers;
using ACTS.UI.Areas.Admin.Models;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class EmployeeController : Controller
	{
		private IEmployeeRepository repository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public ActionResult Table()
		{
			IEnumerable<Employee> employees = repository.Employees.OrderBy(em => em.EmployeeId);
			return View("TableEmployee", employees);
		}

		public ActionResult Edit(int employeeId)
		{
			Employee employee = repository.GetEmployeeById(employeeId);
			return View("EditEmployee", employee);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Employee employee, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					employee.PhotoMimeType = image.ContentType;
					employee.Photo = new byte[image.ContentLength];
					image.InputStream.Read(employee.Photo, 0, image.ContentLength);
				}
				repository.UpdateEmployee(employee);
				TempData.AddMessage(MessageType.Success, $"Employee \"{employee.FullName}\" successfully saved.");
				return RedirectToAction(nameof(Table), new { area = "Admin" });
			} else
			{
				// there is something wrong with the data values         
				return View("EditEmployee", employee);
			}
		}

		public ActionResult Create()
		{
			return View("CreateEmployee");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Employee employee, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					employee.PhotoMimeType = image.ContentType;
					employee.Photo = new byte[image.ContentLength];
					image.InputStream.Read(employee.Photo, 0, image.ContentLength);
				}
				repository.CreateEmployee(employee);
				TempData.AddMessage(MessageType.Success, $"Employee \"{employee.FullName}\" successfully created.");
				return RedirectToAction(nameof(Table), new { area = "Admin" });
			} else
			{
				// there is something wrong with the data values         
				return View("CreateEmployee", employee);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int employeeId)
		{
			Employee deletedEmployee = repository.DeleteEmployee(employeeId);
			if (deletedEmployee != null)
			{
				TempData.AddMessage(new Message(MessageType.Success, $"Employee \"{deletedEmployee.FullName}\" successfully deleted."));
			}
			return RedirectToAction(nameof(Table));
		}
	}
}
