using System;
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

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
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

			return View(model);
		}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
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
			} else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		#endregion
	}
}