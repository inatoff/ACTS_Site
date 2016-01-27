using ACTS.Core.Identity;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Helpers;
using ACTS.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class MyAccountController : Controller
	{
		private ApplicationUserManager _userManager;

		public MyAccountController()
		{ }

		public MyAccountController(ApplicationUserManager userManager)
		{
			_userManager = userManager;
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		public int CurrentUserId
		{
			get { return User.Identity.GetUserId<int>(); }
		}

		// GET: Admin/Manage
		public ActionResult Index()
		{
			var model = new MyAccountViewModel();
			using (var manager = new ApplicationUserManager())
			{
				ApplicationUser user = manager.FindById(User.Identity.GetUserId<int>());

				model.UserName = model.OldUserName = user.UserName;
				model.Email= model.OldEmail = user.Email;
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangeUserName(ChangeUserNameViewModel model)
		{
			if (ModelState.IsValid)
			{
				var currentUser = await UserManager.FindByIdAsync(CurrentUserId);
				currentUser.UserName = model.UserName;

				var result = await UserManager.UpdateAsync(currentUser);

				if (result.Succeeded)
					TempData.AddMessage(new Message(MessageType.Success,
						$"You have successfully changed your username from \"{model.OldUserName}\" on \"{model.UserName}\"."));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
		{
			// TODO: добавить отправку письма с потверждением. 

			//if (ModelState.IsValid)
			//{
			//	var currentUser = await UserManager.FindByIdAsync(CurrentUserId);
			//	currentUser.Email = model.Email;

			//	var result = await UserManager.UpdateAsync(currentUser);

			//	if (result.Succeeded)
			//		TempData.AddMessage(new Message(MessageType.Success,
			//			$"You have successfully changed your username from \"{model.OldUserName}\" on \"{model.UserName}\"."));
			//	else
			//		TempData.AddMessages(MessageType.Warning, result.Errors);
			//}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await UserManager.ChangePasswordAsync(CurrentUserId, model.CurrentPassword, model.NewPassword);

				if (result.Succeeded)
					TempData.AddMessage(new Message(MessageType.Success,
						$"You have successfully changed your password."));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}