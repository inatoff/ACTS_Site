using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;

namespace ACTS.UI.Areas.Admin.Controllers
{
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
				TempData["infoMessage"] = string.Format("{0} has been saved.", employee.FullName);
				return RedirectToAction(nameof(Table), new { area = "Admin" });
			} else
			{
				// there is something wrong with the data values         
				return View("EditEmployee", employee);
			}
		}

		public ActionResult Create()
		{
			ViewBag.CurrentTreeView = "Create";
			return View("CreateEmployee");
		}

		[HttpPost]
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
				TempData["infoMessage"] = string.Format("{0} has been updated.", employee.FullName);
				return RedirectToAction(nameof(Table), new { area = "Admin" });
			} else
			{
				// there is something wrong with the data values         
				return View("CreateEmployee", employee);
			}
		}

		[HttpPost]
		public ActionResult Delete(int employeeId)
		{
			Employee deletedEmployee = repository.DeleteEmployee(employeeId);
			if (deletedEmployee != null)
			{
				TempData["infoMessage"] = ($"{deletedEmployee.FullName} was deleted.");
			}
			return RedirectToAction(nameof(Table));
		}
	}
}
