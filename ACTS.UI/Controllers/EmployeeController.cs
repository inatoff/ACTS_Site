using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Abstract;
using ACTS.Core.Entities;

namespace ACTS.UI.Controllers
{
	public class EmployeeController : BaseController
	{
		private IEmployeeRepository repository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public FileContentResult GetImage(int employeeID)
		{
			Employee news = repository.Employees.FirstOrDefault(p => p.EmployeeId == employeeID);
			if (news != null)
			{
				return File(news.Photo, news.PhotoMimeType);
			} else
			{
				return null;
			}
		}
	}
}
