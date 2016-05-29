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
using ACTS.UI.Controllers;
using Ninject.Extensions.Logging;
using ACTS.Localization.Resources;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class EmployeeController : BaseController
	{
		private IEmployeeRepository _repository;
		private readonly ILogger _logger;
		public EmployeeController(IEmployeeRepository employeeRepository, ILoggerFactory loggerFactory)
		{
			_repository = employeeRepository;
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public ActionResult Table()
		{
			IEnumerable<Employee> employees = _repository.Employees.OrderBy(em => em.EmployeeId).ToList();
			return View("TableEmployee", employees);
		}

		public ActionResult Edit(int employeeId)
		{
			Employee employee = _repository.GetEmployeeById(employeeId);
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
				_repository.UpdateEmployee(employee);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EmployeeSavedMsg, employee.FullName));
				_logger.Info("Employee \"{0}\" saved by {1}.", employee.FullName, User.Identity.Name);

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
				_repository.CreateEmployee(employee);
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EmployeeSavedMsg, employee.FullName));
				_logger.Info("Employee \"{0}\" created by {1}.", employee.FullName, User.Identity.Name);

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
			Employee deletedEmployee = _repository.DeleteEmployee(employeeId);
			if (deletedEmployee != null)
			{
				TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.EmployeeDeletedMsg, deletedEmployee.FullName));
				_logger.Info("Employee \"{0}\" deleted by {1}.", deletedEmployee.FullName, User.Identity.Name);
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
