using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.UI.Areas.Peoples.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Controllers
{
    [Authorize(Roles = "Teacher, Admin")]
    public class ProfileController : Controller
    {
        // GET: Peoples/Profile
        private ITeacherRepository repository;

        public ProfileController(ITeacherRepository repo)
        {
            repository = repo;
        }

        private ApplicationUserManager UserManager
        {
            get { return new ApplicationUserManager(); }
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            TeacherAccountViewModel model;
            using (var userManager = UserManager)
            {
                var userId = User.Identity.GetUserId<int>();
                var currentTeacher = repository.GetTeacherById(userId);
                var currentUser = await userManager.FindByIdAsync(userId);

                model = new TeacherAccountViewModel()
                {
                    //Part of account info
                    AccountEmail = currentUser.Email,
                    //Part of teacher info
                    Email = currentTeacher.Email,
                    NameSlug = currentTeacher.NameSlug,
                    Degree = currentTeacher.Degree,
                    Intellect = currentTeacher.Intellect,
                    Facebook = currentTeacher.Facebook,
                    Vk = currentTeacher.Vk,
                    Twitter = currentTeacher.Twitter,
                    ScienceInterests = currentTeacher.ScienceInterests,
                    Disciplines = currentTeacher.Disciplines,
                    Projects = currentTeacher.Projects,
                    Publications = currentTeacher.Publications,
                    HasBlog = currentTeacher.Blog == null
                };
            }

            return View();
        }
    }
}