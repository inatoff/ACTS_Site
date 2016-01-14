using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	//[Security(Roles = "Admin", RedirectUrl = "Admin/Account/Login")]
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		public ActionResult Dashboard()
		{
			return View();
		}
	}
}