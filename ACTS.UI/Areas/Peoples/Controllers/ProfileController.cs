using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.Core.Identity;
using ACTS.UI.Areas.Peoples.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Extensions.Logging;
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
        private readonly ILogger _logger;
        public ProfileController(ITeacherRepository teacherRepo, ILoggerFactory loggerFactory)
        {
            teacherRepository = teacherRepo;
            _logger = loggerFactory.GetCurrentClassLogger();
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
                var currentTeacher = teacherRepository.GetTeacher(currentUser.Teacher.TeacherId);

                model = new TeacherAccountViewModel()
                {
                    //Part of account info
                    AccountEmail = currentUser.Email,
                    //Part of teacher info 
                    Email = currentTeacher.Email,
                    Slug = currentTeacher.NameSlug,
                    Degree = currentTeacher.Degree,
                    Greetings = currentTeacher.Greetings,
                    Intellect = currentTeacher.Intellect,
                    Facebook = currentTeacher.Facebook,
                    Vk = currentTeacher.Vk,
                    Twitter = currentTeacher.Twitter,
                    ScienceInterests = currentTeacher.ScienceInterests,
                    Disciplines = currentTeacher.Disciplines,
                    Projects = currentTeacher.Projects,
                    Publications = currentTeacher.Publications,
                    HasBlog = currentTeacher.Blog != null
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
                var currentTeacher = teacherRepository.GetTeacher(currentUser.Teacher.TeacherId);
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
                                _logger.Trace($"{currentUser.UserName} succesfully changed password");
                            }
                            else
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError("Password", item);
                                }
                                _logger.Trace($"{currentUser.UserName} failed password change");
                            }
                        }
                        //TODO: Messenger
                        currentTeacher.Degree = model.Degree;
                        currentTeacher.Email = model.Email;
                        currentTeacher.Greetings = model.Greetings;
                        currentTeacher.Intellect = model.Intellect;
                        currentTeacher.Vk = model.Vk;
                        currentTeacher.Facebook = model.Facebook;
                        currentTeacher.Twitter = model.Twitter;
                        currentTeacher.Disciplines = model.Disciplines;
                        currentTeacher.Projects = model.Projects;
                        currentTeacher.Publications = model.Publications;
                        currentTeacher.ScienceInterests = model.ScienceInterests;
                        teacherRepository.UpdateTeacher(currentTeacher);
                        _logger.Trace($"Teacher {currentTeacher.FullName} updated profile");
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

        public async Task<RedirectToRouteResult> InitPersonalPage()
        {
            using (var userManager = UserManager)
            {
                var currentId = CurrentUserId;
                var currentUser = await userManager.FindByIdAsync(currentId);
                if (currentUser.Teacher.Blog != null)
                {
                    //TODO: Message you already have a blog
                    return RedirectToAction("PersonalPage", "Blog", new { nameSlug = currentUser.Teacher.NameSlug });
                }
                else
                {
                    await teacherRepository.InitPersonalPage(currentUser.Teacher);
                    _logger.Trace($"{currentUser.Teacher.FullName} initialized personal page");
                    return RedirectToAction("PersonalPage", "Blog", new { nameSlug = currentUser.Teacher.NameSlug });
                }
            }
        }
    }
}