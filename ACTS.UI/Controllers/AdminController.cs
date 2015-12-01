using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class AdminController : BaseController
	{
		// GET: Admin
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult AddEmployee()
        {
            return View();
        }
        
	}
}