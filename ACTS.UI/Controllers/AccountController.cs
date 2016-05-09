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
using ACTS.UI.Infrastructure;
using ACTS.Localization.Resources;
using Ninject.Extensions.Logging;
using ACTS.UI.Services;

namespace ACTS.UI.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly ILogger _logger;

        public AccountController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetCurrentClassLogger();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ILoggerFactory loggerFactory)
            : this(loggerFactory)
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
                    ModelState.AddModelError("", GlobalRes.NotEnoughRightsMsg);

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
                string userName = model.EmailOrUserName;

                // не уверен что валидация емейла хоть как то увеличит производительность
                // возможно даже на оборот
                //if (MailHelper.IsValidEmail(model.EmailOrLogin))
                //{
                var user = await UserManager.FindByEmailAsync(model.EmailOrUserName);

                if (user != null)
                    userName = user.UserName;
                //}


                var signInResult = await SignInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, shouldLockout: true);

                switch (signInResult)
                {
                    case SignInStatus.Success:
                        _logger.Trace($"User {userName} is logged in.");
                        return RedirectToLocal(returnUrl);

                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("", GlobalRes.AccountLockedMsg);
                        _logger.Trace($"Account with username \"{userName}\" locked. The number of attempts has exceeded {UserManager.MaxFailedAccessAttemptsBeforeLockout}.");
                        break;

                    //case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                    case SignInStatus.Failure:
                        ModelState.AddModelError("", GlobalRes.IncorrectUserNameOrPasswordMsg);
                        _logger.Trace($"Failed login attempt with EmailOrUserName = {model.EmailOrUserName}.");
                        break;

                    // я думаю это не достижимый код, но пускай будет
                    // все правильно, лучше перебдеть, чем недобдеть. 
                    default:
                        ModelState.AddModelError("", GlobalRes.FailedLoginAttemptsMsg);
                        _logger.Trace($"Failed login attempt with EmailOrUserName = {model.EmailOrUserName}.");
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
            var userName = User.Identity.Name;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _logger.Trace($"User {userName} is logged out.");
            
            return RedirectToAction("About", "Department");
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string userName)
        {
            ApplicationUser user;
            user = UserManager.FindByName(userName);
            return Json(user == null);
        }

        [AllowAnonymous]
        public PartialViewResult AuthenticationLink()
        {
            var item = new MenuLinkItem();
            if (User.Identity.IsAuthenticated && User.IsInRole("Teacher"))
            {
                item.Text = GlobalRes.BlogEditProfile;
                item.Action = "Edit";
                item.RouteInfo = new { controller = "Profile", area = "Peoples" };
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                item.Text = GlobalRes.Administration;
                item.Action = "Dashboard";
                item.RouteInfo = new { controller = "Admin", area = "Admin" };
            }
            else
            {
                item.Text = GlobalRes.Login;
                item.Action = "Login";
                item.RouteInfo = new { controller = "Account", area = "" };
            }
            return PartialView("MenuLinkItem", item);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
                _logger.Trace($"User \"{user.UserName}\" send request to reset password.");

                //Дополнительные сведения о том, как включить подтверждение учетной записи и сброс пароля, см.по адресу: http://go.microsoft.com/fwlink/?LinkID=320771
                //Отправка сообщения электронной почты с этой ссылкой
                string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token }, protocol: Request.Url.Scheme);

                var emailModel = new
                {
                    UserName = user.UserName,
                    CallbackUrl = callbackUrl
                };

                string body = await EmailBodyServiceFactory.GetEmailBody(emailModel, "ForgotPassword");
                await UserManager.SendEmailAsync(user.Id, "Відновлення пароля", body);
                _logger.Debug($"Send verification email to {user.UserName} for reset password.");

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
                {
                    _logger.Info($"User \"{User.Identity.Name}\" reseted password.");
                    return View("Login");
                }

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
                return RedirectToAction("About", "Department");
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