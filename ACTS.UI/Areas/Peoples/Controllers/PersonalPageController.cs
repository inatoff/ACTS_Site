using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using ACTS.UI.Areas.Peoples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Controllers
{   
    public class PersonalPageController : Controller
    {
        private EFTeacherRepository _teacherRepo;
        private EFBlogRepository _blogRepo; 
        // GET: Peoples/PersonalPage 
        public ActionResult Index(string nameSlug)
        {
            var model = GetTeacher(nameSlug);
            ViewBag.Name = model.Result.FullName;
            ViewBag.Degree = model.Result.Degree;
            return View(model);
        }
         
        public ActionResult Publications(string nameSlug)
        {
            List<string> model = GetTeacher(nameSlug).Result.Publications;
            return View(model);
        }
         
        public ActionResult Projects(string nameSlug)
        {
            List<string> model = GetTeacher(nameSlug).Result.Projects;
            return View(model);
        }

        public ActionResult Disciplines(string nameSlug)
        {
            List<string> model = GetTeacher(nameSlug).Result.Disciplines;
            return View(model);
        }

        public ActionResult ScienceInterests(string nameSlug)
        {
            List<string> model = GetTeacher(nameSlug).Result.ScienceInterests;
            return View(model);
        }
        
        private async Task<PersonalPageViewModel> GetTeacher(string nameSlug)
        {
            return new PersonalPageViewModel(await _teacherRepo.GetTeacherByUrlSlugAsync(nameSlug));
        }
    }
}