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
using ACTS.Core.Abstract;
using Microsoft.AspNet.Identity;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private ITeacherRepository _teacherRepository;
		public AccountController(ITeacherRepository teacherRepository)
		{
			_teacherRepository = teacherRepository;
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
			IEnumerable<InfoAccountViewModel> model;
			using (var userManager = UserManager)
			{
				var users = userManager.Users;

				var tasks = users.Select(async delegate (ApplicationUser user)
				{
					InfoAccountViewModel infoAccount;

					using (var perQueryManager = UserManager)
						infoAccount = new InfoAccountViewModel
						{
							Id = user.Id,
							Email = user.Email,
							UserName = user.UserName,
							TeacherName = user.Teacher?.FullName,
							Roles = await perQueryManager.GetRolesAsync(user.Id)
						};

					return infoAccount;
				});

				model = await Task.WhenAll(tasks);
			}

			return View("TableAccount", model);
		}

		private void InitTeachersItems(object selectedValue = null)
		{
			SelectList teachersItems;
			if (selectedValue == null)
				teachersItems = new SelectList(_teacherRepository.NoPairTeachers, "TeacherId", "FullName");
			else
				teachersItems = new SelectList(_teacherRepository.GetNoPairTeachersWithSelected((int)selectedValue).AsEnumerable(),
											  "TeacherId", "FullName", selectedValue);

			ViewBag.Teachers = teachersItems;
		}
		
		[HttpGet]
		public ActionResult Create()
		{
			var model = new AccountViewModel();
			using (var manager = RoleManager)
				model.Roles = manager.Roles.Select(r => new RoleItem() { Value = r.Name })
										   .OrderBy(r => r.Value)
										   .ToList();

			InitTeachersItems();

			return View("CreateAccount", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(AccountViewModel model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser() {
					Email = model.Email,
					UserName = model.UserName,
					LockoutEnabled = true
				};

				using (var manager = UserManager)
				{
					var result = await manager.CreateAsync(user, model.Password);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.AddModelError("", error);

						InitTeachersItems(model.PairTeacherId);

						return View("CreateAccount", model);
					}

					if (model.PairTeacherId.HasValue)
						_teacherRepository.AddPairToUser(model.PairTeacherId.Value, user.Id);

					if (!result.Succeeded)
						TempData.AddMessages(MessageType.Warning, result.Errors);

					result = await manager.AddToRolesAsync(user.Id, model.SelectedRoles.ToArray());

					TempData.AddMessage(new Message(MessageType.Success, $"User \"{user.UserName}\" successfully created."));
				}

				return RedirectToAction(nameof(Table));
			}

			InitTeachersItems(model.PairTeacherId);
			// there is something wrong with the data values         
			return View("CreateAccount", model);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int Id)
		{
			EditAccountViewModel model;
			using (var userManager = UserManager)
			{
				var user = await userManager.FindByIdAsync(Id);

				if (user == null)
				{
					TempData.AddMessage(MessageType.Warning, $"User with ID = {Id} was not found.");
					return RedirectToAction(nameof(Table));
				}

				using (var roleManager = RoleManager)
				{
					var roles = roleManager.Roles.AsEnumerable();

					var userRoles = await userManager.GetRolesAsync(Id);

					model = new EditAccountViewModel()
					{
						Id = user.Id,
						Email = user.Email,
						UserName = user.UserName,
						Roles = roles.Select(r => new RoleItem() { Value = r.Name, Selected = userRoles.Contains(r.Name) }).ToList()
					};
				}

				InitTeachersItems(user.Teacher?.TeacherId);
			}

			return View("EditAccount", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditAccountViewModel model)
		{
			if (ModelState.IsValid)
			{
				using (var manager = UserManager)
				{
					var user = await manager.FindByIdAsync(model.Id);

					if (user == null)
					{
						TempData.AddMessage(MessageType.Danger, $"User with ID = {model.Id} was not found.");
						return RedirectToAction(nameof(Table));
					}

					user.UserName = model.UserName;
					user.Email = model.Email;

					var result = await manager.UpdateAsync(user);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.AddModelError("", error);

						InitTeachersItems(model.PairTeacherId);

						return View("EditAccount", model);
					}

					var userRoles = await manager.GetRolesAsync(model.Id); // роли редактируемого юзера

					result = await manager.RemoveFromRolesAsync(model.Id, userRoles.Intersect(model.UnselectedRoles).ToArray());

					if (!result.Succeeded)
						TempData.AddMessages(MessageType.Warning, result.Errors);

					result = await manager.AddToRolesAsync(model.Id, model.SelectedRoles.Except(userRoles).ToArray());

					if (!result.Succeeded)
						TempData.AddMessages(MessageType.Warning, result.Errors);

					if (user.HasTeacher)
						_teacherRepository.RemovePairToUser(user.Teacher.TeacherId);
					if (model.PairTeacherId.HasValue)
						_teacherRepository.AddPairToUser(model.PairTeacherId.Value, user.Id);

					TempData.AddMessage(new Message(MessageType.Success, $"User \"{user.UserName}\" successfully saved."));
				}

				return RedirectToAction(nameof(Table));
			}

			InitTeachersItems(model.PairTeacherId);
			// there is something wrong with the data values
			return View("EditAccount", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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
					TempData.AddMessages(MessageType.Warning, result.Errors);
			}

			return RedirectToAction(nameof(Table));
		}
	}
}
