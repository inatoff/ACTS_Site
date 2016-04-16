using ACTS.Core.Identity;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using ACTS.UI.Infrastructure;
using ACTS.UI.Models;
using ACTS.UI.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Extensions.Logging;
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
	public class MyAccountController : BaseController
	{
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;
		private readonly ILogger _logger;

		public MyAccountController(ILoggerFactory loggerFactory)
			: base()
		{
			_logger = loggerFactory.GetCurrentClassLogger();
		}

		public MyAccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ILoggerFactory loggerFactory)
			: this(loggerFactory)
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
				model = new MyAccountViewModel(user.UserName, user.Email ?? string.Empty);
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
				{
					TempData.AddMessage(MessageType.Success,
						string.Format(GlobalRes.ChangedUserNameMsg, model.CurrentUserName, model.UserName));
					_logger.Info("User \"{0}\" changed username on \"{1}\".", model.CurrentUserName, model.UserName);
				}
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ConfirmChangeEmail(ChangeEmailViewModel model)
		{
			_logger.Debug("User \"{0}\" send request to change email from \"{0}\" on \"{1}\".", model.CurrentEmail, model.Email);
			int userId = CurrentUserId;

			var user = await UserManager.FindByIdAsync(userId);

			if (string.IsNullOrWhiteSpace(user.Email))
			{
				var result = await UserManager.SetEmailAsync(userId, model.Email);

				if (result.Succeeded)
					TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.ChangedEmailMsg, model.Email));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);

				return RedirectToAction(nameof(Index));
			}

			string token = await UserManager.GenerateUserTokenAsync("ChangeEmail", userId);

			var emailModel = new
			{
				UserName = user.UserName,
				model.Email,
				CallbackUrl = Url.Action("ChangeEmail", "MyAccount", new { userId, email = model.Email, token }, Request.Url.Scheme)
			};

			string body = await EmailBodyServiceFactory.GetEmailBody(emailModel, "ConfirmChangeEmail");

			await UserManager.SendEmailAsync(CurrentUserId, "Змінити емейл", body);

			TempData.AddMessage(MessageType.Info, string.Format(GlobalRes.SendVerificationEmailMsg, model.CurrentEmail));
			_logger.Debug("Send verification email to {1} for change email by request \"{0}\".", user.UserName, model.CurrentEmail);

			return RedirectToAction(nameof(Index));
		}

		public async Task<ActionResult> ChangeEmail(int userId, string email, string token)
		{
			if (await UserManager.VerifyUserTokenAsync(userId, "ChangeEmail", token))
			{
				var result = await UserManager.SetEmailAsync(userId, email);

				if (result.Succeeded)
					TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.ChangedEmailMsg, email));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}
			else
				TempData.AddMessage(MessageType.Warning, 
					string.Format(GlobalRes.FailedChangeEmailMsg, email, Environment.NewLine));
			_logger.Info("User \"{0}\" changed email on \"{1}\".", User.Identity.Name, email);

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
				{
					TempData.AddMessage(MessageType.Success, GlobalRes.ChangedPasswordMsg);
					_logger.Info("User \"{0}\" changed password.", User.Identity.Name);
				}
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
					TempData.AddMessage(MessageType.Warning, GlobalRes.CanNotDeleteOnlyAdminUser);
					return RedirectToAction(nameof(Index));
				}

				var user = await UserManager.FindByIdAsync(CurrentUserId);

				var result = await UserManager.DeleteAsync(user);

				if (!result.Succeeded)
				{
					TempData.AddMessages(MessageType.Warning, result.Errors);
					return RedirectToAction(nameof(Index));
				}

				HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

				_logger.Info("User \"{0}\" deleted himself.", User.Identity.Name);

				return RedirectToAction(nameof(Index), "Home", new { area = "" });
			}

			return RedirectToAction(nameof(Index));
		}
	}
}