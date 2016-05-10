using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Peoples.Models;
using ACTS.UI.Helpers;
using ACTS.UI.Models;
using Microsoft.AspNet.Identity;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Peoples/Blog
        private readonly IBlogRepository _blogRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly ILogger _logger;
        public ApplicationUserManager UserManager
        {
            get { return new ApplicationUserManager(); }
        }

        public int CurrentUserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        public BlogController(IBlogRepository blogRepo, ITeacherRepository teacherRepo, ILoggerFactory loggerFactory)
        {
            _blogRepo = blogRepo;
            _teacherRepo = teacherRepo;
            _logger = loggerFactory.GetCurrentClassLogger();
        }

        [AllowAnonymous]
        public async Task<ActionResult> PersonalPage(string nameSlug)
        {           
            var model = await GetPersonalPage(nameSlug);
            ViewBag.Teacher = model;
            if (model != null) return View(model);
            else return new HttpNotFoundResult();
        }

        [AllowAnonymous]
        public ActionResult Blog(string nameSlug)
        {
            var blog = _blogRepo.GetBlogByAuthorNameSlug(nameSlug);
            return blog != null ? View(blog) : (ActionResult)HttpNotFound();
        }

        [HttpGet]
        [Authorize(Roles ="Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Create(Post model)
        {
            using (var userManager = UserManager)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await userManager.FindByIdAsync(CurrentUserId);
                var teacher = _teacherRepo.GetTeacherById(user.Teacher.TeacherId);
                model.Slug = model.Title.MakeUrlFriendly();
                model.Created = DateTime.Now;
                model.Blog = teacher.Blog;
                try
                {
                    _blogRepo.CreatePost(model);

                    return RedirectToAction("index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("key", e);
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Route("edit/{postId}")]
        public async Task<ActionResult> Edit(int postId)
        {
            var post = await _blogRepo.GetPostByIdAsync(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }
        
        [HttpPost]
        [Route("edit/{postId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(int postId, Post model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var userManager = UserManager)
            {
                var postAuthorId = model.Blog.TeacherId;
                var currentUserTeacherId = await _teacherRepo.GetCurrentUserTeacherIdAsync(CurrentUserId);
                if (postAuthorId != currentUserTeacherId)
                {
                    return new HttpUnauthorizedResult();
                }
                try
                {
                    model.Modified = DateTime.Now;
                    model.Slug = model.Title.MakeUrlFriendly();
                    await _blogRepo.EditPost(postId, model);

                    return RedirectToAction("index", new { nameSlug = model.Blog.Teacher.NameSlug, postSlug = model.Slug });
                }
                catch (KeyNotFoundException e)
                {
                    return HttpNotFound();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Delete(int postId)
        {
            var post = await _blogRepo.GetPostByIdAsync(postId);

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult> Delete(int postId, string foo)
        {
            try
            {
                var post = await _blogRepo.GetPostByIdAsync(postId);
                var teacherNameSlug = post.Blog.Teacher.NameSlug;
                _blogRepo.DeletePost(postId);
                return RedirectToAction("index", new { nameSlug = teacherNameSlug });
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> GetPersonalPageViewModelJsonAsync(string nameSlug)
        {
            var model = await GetPersonalPage(nameSlug);
            if (model != null) return Json(model);
            else return new HttpNotFoundResult();
        }

        private async Task<PersonalPageViewModel> GetPersonalPage(string nameSlug)
        {
            var model = await _teacherRepo.GetTeacherByUrlSlugAsync(nameSlug);
            if (model != null) { return new PersonalPageViewModel(model); }
            else return null;
        }

        [AllowAnonymous]
        public PartialViewResult Navigation(string name)
        {
            List<MenuLinkItem> Items = new List<MenuLinkItem>();
            if (User.IsInRole("Teacher") && User.Identity.Name == name)
            {
                Items.Add(new MenuLinkItem()
                {
                    Text = GlobalRes.BlogEditProfile,
                    Action = "Edit",
                    RouteInfo = new { controller = "Profile", area = "Peoples" }
                });
                Items.Add(new MenuLinkItem()
                {
                    Text = GlobalRes.Logout,
                    Action = "Logout",
                    RouteInfo = new { controller = "Account", area = "" }
                });
            }
            return PartialView("MenuLinkItem", Items);

        }
    }
}
