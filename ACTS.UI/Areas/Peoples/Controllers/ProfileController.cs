using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Peoples/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}