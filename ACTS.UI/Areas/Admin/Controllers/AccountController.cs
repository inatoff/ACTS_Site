using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ACTS.UI.Models;
using System.Threading.Tasks;
using ACTS.Core.Identity;
using ACTS.UI.Areas.Admin.Models;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

		public AccountController()
		{
		}

		public AccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
		{
			UserManager = userManager;
            RoleManager = roleManager;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public async Task<ActionResult> Table()
		{
			//Func<ApplicationUser, Task<AccountInfoViewModel>> funk = async (user) => {
			//	return new AccountInfoViewModel {
			//		Id = user.Id,
			//		Email = user.Email,
			//		UserName = user.UserName,
			//		TeacherKey = user.TeacherKey,
			//		Roles = await UserManager.GetRolesAsync(user.Id)
			//	};
			//};

			var tasks = UserManager.Users
				.Select(async delegate (ApplicationUser user) {
					return new AccountInfoViewModel {
						Id = user.Id,
						Email = user.Email,
						UserName = user.UserName,
						TeacherKey = user.TeacherKey,
						Roles = await UserManager.GetRolesAsync(user.Id)
					};
				});

			var users = await Task.WhenAll(tasks);

			return View("TableAccount", users);
		}

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CurrentTreeView = "Create";
            ViewBag.AvailabeRoles = RoleManager.Roles.Select(r=>r.Name);
            return View("CreateAccount");
        }

        [HttpPost]
        public async Task<ActionResult> Create(AccountViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = user.Email,
                    UserName = user.UserName,

                    LockoutEnabled = true
                };



                await _userManager.CreateAsync(user);
                return RedirectToAction(nameof(Table));
            }
            else
            {
                // there is something wrong with the data values         
                return View("CreateEmployee"/*, employee*/);
            }
        }

    }
}