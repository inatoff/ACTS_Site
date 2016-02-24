using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using Ninject.Extensions.Logging;
using ACTS.UI.Infrastructure;

namespace ACTS.UI.Areas.Admin.Controllers
{
	//[Security(Roles = "Admin", RedirectUrl = "Admin/Account/Login")]
	[Authorize(Roles = "Admin")]
	public class AdminController : BaseController
	{
		private readonly ILogger _logger;
		public AdminController(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public ActionResult Dashboard()
		{
			return View();
		}

		public ActionResult Test()
		{
			_logger.Debug("hello from Admin:TestAction");
			throw new ApplicationException(message: "test");
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