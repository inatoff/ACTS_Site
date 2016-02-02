using ACTS.Core.Identity;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Helpers;
using ACTS.UI.Infrastructure;
using ACTS.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class MyAccountController : Controller
	{
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;

		public MyAccountController()
			: base()
		{
		}

		public MyAccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
			: this()
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			set
			{
				_userManager = value;
			}
		}

		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
			}
			set
			{
				_roleManager = value;
			}
		}

		public int CurrentUserId
		{
			get { return User.Identity.GetUserId<int>(); }
		}

		// GET: Admin/Manage
		public ActionResult Index()
		{
			MyAccountViewModel model;
			using (var manager = new ApplicationUserManager())
			{
				ApplicationUser user = manager.FindById(CurrentUserId);
				model = new MyAccountViewModel(user.UserName, user.Email);
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
						$"You have successfully changed your username from \"{model.CurrentUserName}\" on \"{model.UserName}\"."));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ConfirmChangeEmail(ChangeEmailViewModel model)
		{
			int userId = CurrentUserId;

			string token = await UserManager.GenerateUserTokenAsync("ChangeEmail", userId);
			var user = await UserManager.FindByIdAsync(userId);

			var emailModel = new
			{
				user.UserName,
				model.Email,
				CallbackUrl = Url.Action("ChangeEmail", "MyAccount", new { userId, email = model.Email, token }, Request.Url.Scheme)
			};

			string body = EmailBodyFactory.GetEmailBody(emailModel, "ConfirmChangeEmail");

			await UserManager.SendEmailAsync(CurrentUserId, "Змінити емейл", body);

			TempData.AddMessage(MessageType.Info, $"We sent a verification email to {model.CurrentEmail}. Please follow the instructions in it.");

			return RedirectToAction(nameof(Index));
		}

		public async Task<ActionResult> ChangeEmail(int userId, string email, string token)
		{
			if (await UserManager.VerifyUserTokenAsync(userId, "ChangeEmail", token))
			{
				var result = await UserManager.SetEmailAsync(userId, email);

				if (result.Succeeded)
					TempData.AddMessage(new Message(MessageType.Success, $"You have successfully changed your email on \"{email}\"."));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}
			else
				TempData.AddMessage(MessageType.Warning, $"You failed change the email on \"{email}\".{Environment.NewLine}Maybe it happened because of the expiration of the allotted 24 hours to perform the operation.");

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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteCurrent(DeleteCurrentViewModel model)
		{
			if (ModelState.IsValid)
			{
				if ((await RoleManager.FindByNameAsync("Admin")).Users.Count() < 2)
				{
					TempData.AddMessage(MessageType.Warning, $"You can not delete the only user with administrative rights.");
					return RedirectToAction(nameof(Index));
				}

				var user = await UserManager.FindByIdAsync(CurrentUserId);

				var result = await UserManager.DeleteAsync(user);

				if (!result.Succeeded)
				{
					TempData.AddMessages(MessageType.Warning, result.Errors);
					return RedirectToAction(nameof(Index));
				}

				return RedirectToAction(nameof(Index), "Home", new { area = "" });
			}

			return RedirectToAction(nameof(Index));
		}
	}
}