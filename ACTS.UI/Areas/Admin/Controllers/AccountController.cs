using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ACTS.UI.Models;
using System.Threading.Tasks;
using ACTS.Core.Identity;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private ApplicationUserManager _userManager;

		public AccountController()
		{
		}

		public AccountController(ApplicationUserManager userManager)
		{
			UserManager = userManager;
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
	}
}