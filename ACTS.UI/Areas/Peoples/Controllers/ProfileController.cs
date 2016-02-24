using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.Core.Identity;
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
        private ITeacherRepository teacherRepository;
        private IBlogRepository blogRepository;

        public ProfileController(ITeacherRepository teacherRepo, IBlogRepository blogRepo)
        {
            teacherRepository = teacherRepo;
            blogRepository = blogRepo;
            
        }

        public int CurrentUserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        private ApplicationUserStore UserStore
        {
            get { return new ApplicationUserStore(new EFDbContext()); }
        }

        private ApplicationUserManager UserManager
        {
            get { return new ApplicationUserManager(UserStore); }
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            TeacherAccountViewModel model;
            using (var userManager = UserManager)
            {
                var currentUserId = CurrentUserId;
                var currentUser = await userManager.FindByIdAsync(currentUserId);
                var currentTeacher = teacherRepository.GetTeacherById(currentUser.Teacher.TeacherId);

                model = new TeacherAccountViewModel()
                {
                    //Part of account info
                    AccountEmail = currentUser.Email,
                    //Part of teacher info 
                    Email = currentTeacher.Email,
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

            return View("Edit",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeacherAccountViewModel model)
        {
            using (var userManager = UserManager)
            {
                var currentUserId = CurrentUserId;
                var currentUser = await userManager.FindByIdAsync(currentUserId);
                var currentTeacher = teacherRepository.GetTeacherById(currentUser.Teacher.TeacherId);
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    ModelState.AddModelError("PageError", "Password must be supplied");
                }
                else
                {
                    if (ModelState.IsValid && await CheckCurrentPasswordAsync(currentUserId, model.Password))
                    {
                        if (!string.IsNullOrWhiteSpace(model.NewPassword))
                        {
                            var result = await userManager.ChangePasswordAsync(currentUserId, model.Password, model.NewPassword);
                            if (result.Succeeded)
                            {
                                //TODO: Messenger
                            }
                            else
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError("Password", item);
                                }
                            }
                        }
                        //TODO: Messenger
                        teacherRepository.UpdateTeacherByProfile(currentTeacher.TeacherId, new Teacher
                        {
                            Degree = model.Degree,
                            Email = model.Email,

                            Intellect = model.Intellect,
                            Vk = model.Vk,
                            Facebook = model.Facebook,
                            Twitter = model.Twitter,

                            Disciplines = model.Disciplines,
                            Projects = model.Projects,
                            Publications = model.Publications,
                            ScienceInterests = model.ScienceInterests
                        });
                    }
                }
                return View("Edit",model);
            }
        }

        private async Task<bool> CheckCurrentPasswordAsync(int id, string password)
        {
            using (var userManager = UserManager)
            {
                var currentUser = await userManager.FindByIdAsync(id);
                return await userManager.CheckPasswordAsync(currentUser, password);
            }
        }

        private async Task<RedirectToRouteResult> InitPersonalPage()
        {
            using (var userManager = UserManager)
            {
                var currentId = CurrentUserId;
                var currentUser = await userManager.FindByIdAsync(currentId);
                if (currentUser.Teacher.Blog != null)
                {
                    //TODO: Message you already have a blog
                    return RedirectToRoute("ToDefaultPeoplesArea", new { nameSlug = currentUser.Teacher.NameSlug });
                }
                else
                {
                    await blogRepository.InitBlog(currentUser.Teacher);
                    return RedirectToRoute("ToDefaultPeoplesArea", new { nameSlug = currentUser.Teacher.NameSlug });
                }
            }
        }

        
        //[HttpPost]
        //public async Task<ActionResult> Edit(TeacherAccountViewModel model)
        //{

        //}
    }
}