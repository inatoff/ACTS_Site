using ACTS.Core.Abstract;
using ACTS.Core.Identity;
using Microsoft.AspNet.Identity;
using ACTS.UI.App_LocalResources;
using ACTS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Concrete;
using ACTS.Core.Entities;

namespace ACTS.UI.Controllers
{ 
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        private ITeacherRepository _teacherRepository;
        public BlogController(ITeacherRepository teacherRepository, IBlogRepository blogRepository)
        {
            _teacherRepository = teacherRepository;
            _blogRepository = blogRepository;
        }
        // GET: Blog
        [Route("Blog/{nameSlug}")]
        public ActionResult Blog(string nameSlug)
        {
            var teacher = _teacherRepository.GetTeacherByUrlSlug(nameSlug);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher.Blog);
        }

        
        public PartialViewResult BlogMenu(string nameSlug)
        {
            var items = new List<BlogMenuItem>();
            items.Add(new BlogMenuItem()
            {
                Text = GlobalRes.BlogMain,
                Action = "Blog",
                RouteInfo = new { controller = "blog" }
            });
            items.Add(new BlogMenuItem()
            {
                Text = GlobalRes.BlogInterests,
                Action = "Interests",
                RouteInfo = new { controller = "blog" }
            });
            items.Add(new BlogMenuItem()
            {
                Text = GlobalRes.BlogPublications,
                Action = "Publications",
                RouteInfo = new { controller = "blog" }
            });
            items.Add(new BlogMenuItem()
            {
                Text = GlobalRes.BlogProjects,
                Action = "Projects",
                RouteInfo = new { controller = "blog" }
            });
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Teacher"))
                {
                    var blog = _blogRepository.GetBlogByAuthorNameSlug(nameSlug);
                    if (User.Identity.Name == blog.Teacher.User.UserName)
                    {
                        items.Add(new BlogMenuItem()
                        {
                            Text = GlobalRes.BlogEditProfile,
                            Action = "EditProfile",
                            RouteInfo = new { controller = "blog" }
                        });
                        items.Add(new BlogMenuItem()
                        {
                            Text = GlobalRes.BlogAddPost,
                            Action = "AddPost",
                            RouteInfo = new { controller = "blog" }
                        }); 
                    }
                }
            }
            return PartialView(items);
        } 
    }
}