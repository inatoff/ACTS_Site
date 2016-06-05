using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.Localization.Resources; 
using ACTS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc; 
using Microsoft.AspNet.Identity;

namespace ACTS.UI.Controllers
{
	[RoutePrefix("")]
	public class TeachingStaffController : BaseController
	{
		private ITeacherRepository _repository;
		private ApplicationUserManager _manager;


		public TeachingStaffController(ITeacherRepository employeeRepository, ApplicationUserManager manager)
		{
			_repository = employeeRepository;
			_manager = manager;
		}

		public async Task<ActionResult> GetPersonPhoto(string teacherSlug)
		{
			Teacher person = await _repository.GetTeacherByUrlSlugAsync(teacherSlug);

			if (person != null && person.PhotoId.HasValue)
				return RedirectToAction("Index", "File", new { id = person.PhotoId.Value });

			return null;
		}

		[Route("Teachers")]
		public async Task<ViewResult> TeachingStaff()
		{
			IEnumerable<Teacher> teachers = await _repository.GetAllTeachersAsync();
			return View(teachers);
		}

		[Authorize(Roles = "Teacher")]
		public new ActionResult Profile()
		{
			return RedirectToAction("Profile", "ProfileController", new { area = "Peoples" });
		}
}
}
