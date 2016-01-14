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
using ACTS.Core.Concrete;
using ACTS.UI.Helpers;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{

		public AccountController()
		{
		}

		public ApplicationUserManager UserManager
		{
			get { return new ApplicationUserManager(); }
		}

		public ApplicationRoleManager RoleManager
		{
			get { return new ApplicationRoleManager(); }
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

			var users = UserManager.Users;

			var tasks = users.Select(async delegate (ApplicationUser user) {
				return new InfoAccountViewModel {
					Id = user.Id,
					Email = user.Email,
					UserName = user.UserName,
					TeacherKey = user.TeacherKey,
					Roles = await UserManager.GetRolesAsync(user.Id)
				};
			});


			var model = await Task.WhenAll(tasks);

			return View("TableAccount", model);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var model = new AccountViewModel() {
				Roles = (RoleManager.Roles.Select(r => new RoleItem() { Value = r.Name })
										  .OrderBy(r => r.Value)
										  .ToList())
			};
			return View("CreateAccount", model);
		}

		[HttpPost]
		public async Task<ActionResult> Create(AccountViewModel model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser() {
					Email = model.Email,
					UserName = model.UserName,
					LockoutEnabled = true
				};

				var result = await UserManager.CreateAsync(user, model.Password);

				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
						ModelState.AddModelError("", error);

					return View("CreateAccount", model);
				}

				await UserManager.AddToRolesAsync(user.Id, model.SelectedRoles.ToArray());

				TempData.AddMessage(new Message(MessageType.Success, $"User \"{user.UserName}\" successfully created."));

				return RedirectToAction(nameof(Table));
			}

			// there is something wrong with the data values         
			return View("CreateAccount", model);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int Id)
		{
			var user = await UserManager.FindByIdAsync(Id);

			if (user == null)
			{
				TempData.AddMessage(MessageType.Warning, $"User with ID = {Id} was not found.");
				return RedirectToAction(nameof(Table));
			}

			var roles = RoleManager.Roles.AsEnumerable();

			var userRoles = await UserManager.GetRolesAsync(Id);

			var model = new EditAccountViewModel() {
				Id = user.Id,
				Email = user.Email,
				UserName = user.UserName,
				Roles = roles.Select(r => new RoleItem() { Value = r.Name, Selected = userRoles.Contains(r.Name) }).ToList()
			};

			return View("EditAccount", model);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(EditAccountViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await UserManager.FindByIdAsync(model.Id);

				if (user == null)
				{
					TempData.AddMessage(MessageType.Danger, $"User with ID = {model.Id} was not found.");
					return RedirectToAction(nameof(Table));
				}

				user.UserName = model.UserName;
				user.Email = model.Email;

				var userRoles = await UserManager.GetRolesAsync(model.Id); // роли редактируемого юзера

				var result = await UserManager.RemoveFromRolesAsync(model.Id, userRoles.Intersect(model.UnselectedRoles).ToArray());

				if (!result.Succeeded)
					TempData.AddMessage(MessageType.Warning, result.Errors);

				result = await UserManager.AddToRolesAsync(model.Id, model.SelectedRoles.Except(userRoles).ToArray());//.Where(r => !userRoles.Contains(r) && ).ToArray());

				if (!result.Succeeded)
					TempData.AddMessage(MessageType.Warning, result.Errors);

				TempData.AddMessage(new Message(MessageType.Success, $"User \"{user.UserName}\" successfully saved."));

				return RedirectToAction(nameof(Table));
			}

			// there is something wrong with the data values         
			return View("EditAccount", model);
		}

		[HttpPost]
		public async Task<ActionResult> Delete(int Id)
		{
			using (var manager = UserManager)
			{

				var user = await manager.FindByIdAsync(Id);

				if (user == null)
				{
					TempData.AddMessage(MessageType.Warning, $"User with ID = {Id} was not found.");
					return RedirectToAction(nameof(Table));
				}

				//var rolesForUser = await manager.GetRolesAsync(Id);

				//var result = await manager.RemoveFromRolesAsync(Id, rolesForUser.ToArray());

				//if (!result.Succeeded)
				//{
				//	TempData.AddMessage(MessangeType.Danger, result.Errors);
				//	return RedirectToAction(nameof(Table));
				//}

				var result = await manager.DeleteAsync(user);

				if (result.Succeeded)
					TempData.AddMessage(new Message(MessageType.Success, $"User \"{user.UserName}\" successfully deleted."));
				else
					TempData.AddMessage(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Table));
		}
	}
}
