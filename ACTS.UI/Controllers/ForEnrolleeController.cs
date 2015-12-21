using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
    public class ForEnrolleeController : BaseController
    {
        // GET: Enrollee
        public ActionResult OpenDoors()
        {
            return View();
        }

        public ActionResult ConditionsOfAdmissions()
        {
            return View();
        }

        public ActionResult FDP()
        {
            return View();
        }

        public ActionResult Magistracy()
        {
            return View();
        }
    }
}