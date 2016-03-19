using ACTS.Core.Abstract;
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
        private ITeacherRepository _teacherRepo;
        private IBlogRepository _blogRepo;
        // GET: Peoples/PersonalPage 

        public PersonalPageController(ITeacherRepository teacherRepository, IBlogRepository blogRepository)
        {
            _teacherRepo = teacherRepository;
            _blogRepo = blogRepository;
        }

        public async Task<ActionResult> Index(string nameSlug)
        {
            var model = await GetPersonalPage(nameSlug);
            ViewBag.Name = model.FullName;
            ViewBag.Degree = model.Degree;
            return View(model);
        }

        public async Task<ActionResult> Publications(string nameSlug)
        {
            var model = (await GetPersonalPage(nameSlug)).Publications;
            return View(model);
        }

        public async Task<ActionResult> Projects(string nameSlug)
        {
            var model = (await GetPersonalPage(nameSlug)).Projects;
            return View(model);
        }

        public async Task<ActionResult> Disciplines(string nameSlug)
        {
            var model = (await GetPersonalPage(nameSlug)).Disciplines;
            return View(model);
        }

        public async Task<ActionResult> ScienceInterests(string nameSlug)
        {
            var model = (await GetPersonalPage(nameSlug)).ScienceInterests;
            return View(model);
        }

        private async Task<PersonalPageViewModel> GetPersonalPage(string nameSlug)
        {
            return new PersonalPageViewModel(await _teacherRepo.GetTeacherByUrlSlugAsync(nameSlug));
        }
    }
}