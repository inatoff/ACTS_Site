﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ACTS.UI.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ACTS.UI.Helpers;
using ACTS.Core.Identity;
using ACTS.UI.Infrastructure;
using ACTS.UI.App_LocalResources;

namespace ACTS.UI.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private ApplicationUserManager _userManager;
		private ApplicationSignInManager _signInManager;

		public AccountController()
		{
		}

		public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
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

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		// если таки будет сделано отдельные Login для Admin то можно просто редиректить отсюда туда если 
		// returnUrl.ToLower().StartsWith("/admin")
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			// не уверен что нужно говорить пользователю что он не имеет достаточно прав,
			// но вместе с етим мы можем иметь ситуацию когда кто то битый час пытаеться войти 
			// не зная почему его не пускает
			if (string.IsNullOrWhiteSpace(returnUrl) ? false : returnUrl.ToLower().StartsWith("/admin"))
				if (!User.IsInRole("Admin") && User.Identity.IsAuthenticated)
					ModelState.AddModelError("", "Not enough rights for the current account.");

			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				string userName = model.EmailOrLogin;

				// не уверен что валидация емейла хоть как то увеличит производительность
				// возможно даже на оборот
				//if (MailHelper.IsValidEmail(model.EmailOrLogin))
				//{
				var user = await UserManager.FindByEmailAsync(model.EmailOrLogin);

				if (user != null)
					userName = user.UserName;
				//}


				var signInResult = await SignInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, shouldLockout: true);

				switch (signInResult)
				{
					case SignInStatus.Success:
						return RedirectToLocal(returnUrl);

					case SignInStatus.LockedOut:
						ModelState.AddModelError("", "This account is locked. Try again later.");
						break;

					//case SignInStatus.RequiresVerification:
					//return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

					case SignInStatus.Failure:
						ModelState.AddModelError("", "Username or password is incorrect"/*"Невірний логін або пароль."*/);
						break;

					// я думаю это не достижимый код, но пускай будет
					default:
						ModelState.AddModelError("", "Failed login attempts."/*"Неудачная попытка входа."*/);
						break;
				}
			}

			ViewBag.ReturnUrl = returnUrl;

			return View(model);
		}

		// POST: /Account/Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public JsonResult doesUserNameExist(string userName)
		{
			ApplicationUser user;
			user = UserManager.FindByName(userName);
			return Json(user == null);
		}

		//
		// GET: /Account/ForgotPassword
		[AllowAnonymous]
		public ActionResult ForgotPassword(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
        [AllowAnonymous]
        public PartialViewResult AuthenticationLink()
        {
            var item = new MenuLinkItem();
            if (User.Identity.IsAuthenticated)
            {
                item.Text = GlobalRes.BlogEditProfile;
                item.Action = "Edit";
                item.RouteInfo = new { controller = "Profile", area = "Peoples" };
            }
            else
            {
                item.Text = GlobalRes.Login;
                item.Action = "Login";
                item.RouteInfo = new { controller = "Account", area = ""};
            }
            return PartialView("MenuLinkItem", item);
        }

		//
		// POST: /Account/ForgotPassword
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model, string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			if (ModelState.IsValid)
			{
				var user = await UserManager.FindByEmailAsync(model.Email);
				if (user == null)
					// Не показывать, что пользователь не существует или не подтвержден
					return View("ForgotPasswordConfirmation");

				//Дополнительные сведения о том, как включить подтверждение учетной записи и сброс пароля, см.по адресу: http://go.microsoft.com/fwlink/?LinkID=320771
				//Отправка сообщения электронной почты с этой ссылкой
				string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
				var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token }, protocol: Request.Url.Scheme);

				var emailModel = new
				{
					user.UserName,
					CallbackUrl = callbackUrl
				};

				string body = EmailBodyFactory.GetEmailBody(emailModel, "ForgotPassword");
				await UserManager.SendEmailAsync(user.Id, "Відновлення пароля", body);
				return View("ForgotPasswordConfirmation");
			}

			// Появление этого сообщения означает наличие ошибки; повторное отображение формы
			return View(model);
		}

		//
		// GET: /Account/ResetPassword
		[AllowAnonymous]
		public ActionResult ResetPassword(int userId, string token)
		{
			var model = new ResetPasswordViewModel()
			{
				Token = token,
				UserId = userId
			};

			return token == null ? View("Error") : View(model);
		}

		//
		// POST: /Account/ResetPassword
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await UserManager.ResetPasswordAsync(model.UserId, model.Token, model.NewPassword);
				if (result.Succeeded)
					return View("Login");

				AddErrors(result);
			}

			return View(model);
		}

		#region Helpers
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private async Task SignInAsync(ApplicationUser user, bool isPersistent)
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
			AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
				ModelState.AddModelError("", error);
		}
		#endregion
	}
}