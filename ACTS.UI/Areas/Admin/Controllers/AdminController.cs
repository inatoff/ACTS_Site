using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.Owin;
using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTS.Core.Identity;
using ACTS.UI.Areas.Admin.Models;

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

		//public ActionResult MyAccount()
		//{
		//	var model = new ProfileViewModel();
		//	using (var manager = new ApplicationUserManager())
		//	{
		//		ApplicationUser user = manager.FindById(User.Identity.GetUserId<int>());

		//		ViewBag.ReturnUrl = Url.Action();
		//		model.UserName = user.UserName;
		//		model.Email = user.Email;
		//	}

		//	return View(model);
		//}
	}
}