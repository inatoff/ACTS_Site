using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class TeacherController : BaseController
	{
		private ITeacherRepository repository;

		public TeacherController(ITeacherRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public FileContentResult GetImage(int employeeID)
		{
			Teacher news = repository.Teachers.FirstOrDefault(p => p.TeacherID == employeeID);
			if (news != null)
			{
				return File(news.Photo, news.PhotoMimeType);
			} else
			{
				return null;
			}
		}

        public ViewResult TeachingStaff()
        {
            return View(repository.Teachers);
        }
	}
}