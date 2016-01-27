using ACTS.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Controllers
{
    [RouteArea("Peoples")]
    [RoutePrefix("")]
    public class PersonalPageController : Controller
    {
        private EFTeacherRepository _teacherRepo;
        private EFBlogRepository _blogRepo;
        // GET: Peoples/PersonalPage
        [Route("{nameslug}")]
        public ActionResult Index(string nameSlug)
        {
            return View();
        }
    }
}