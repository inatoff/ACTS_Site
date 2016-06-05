using ACTS.Core.Abstract;
using ACTS.Core.Identity;
using ACTS.Localization.Resources;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Controllers;
using ACTS.UI.Helpers;
using Microsoft.AspNet.Identity;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	[Authorize]
	public class AccountController : BaseController
	{
		private readonly ILogger _logger;
		private ITeacherRepository _teacherRepository;

		public AccountController(ITeacherRepository teacherRepository, ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.GetCurrentClassLogger();
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
			using (ApplicationUserManager userManager = UserManager)
			using (ApplicationRoleManager roleManager = RoleManager)
			{
				//var users = userManager.Users.Include(user => user.Teacher).AsNoTracking();

				//var tasks = users.Select(async delegate (ApplicationUser user)
				//{
				//	InfoAccountViewModel infoAccount;

				//	using (var perQueryManager = UserManager)
				//		infoAccount = new InfoAccountViewModel
				//		{
				//			Id = user.Id,
				//			Email = user.Email,
				//			UserName = user.UserName,
				//			TeacherName = user.Teacher?.FullName,
				//			Roles = await perQueryManager.GetRolesAsync(user.Id)
				//		};

				//	return infoAccount;
				//});

				//model = await Task.WhenAll(tasks);

				var infoAccounts = await userManager.Users
					.Include(user => user.Teacher)
					.AsNoTracking()
					.Select(user => new InfoAccountViewModel
					{
						Id = user.Id,
						Email = user.Email,
						UserName = user.UserName,
						TeacherName = user.Teacher == null ? string.Empty : user.Teacher.FullName,
					}).ToListAsync();

				var roles = roleManager.Roles.Include(r => r.Users).ToList();
				var DictRoleIdName = roles.ToDictionary(r => r.Id, r => r.Name);
				var userIdWithRoleNames = roles.SelectMany(r => r.Users)
					.GroupBy(ur => ur.UserId)
					.ToDictionary(userIdWithRoleIds => userIdWithRoleIds.Key,
								  userIdWithRoleIds => userIdWithRoleIds.Select(roleId => DictRoleIdName[roleId.RoleId]).ToList());

				List<string> rolesNames;
				foreach (var ia in infoAccounts)
					ia.Roles = userIdWithRoleNames.TryGetValue(ia.Id, out rolesNames) ? rolesNames : new List<string>();

				model = infoAccounts.AsEnumerable();
			}

			return View("TableAccount", model);
		}

		[NonAction]
		private void InitTeachersItems(int? selectedValue = null)
		{
			SelectList teachersItems;
			if (selectedValue.HasValue)
				teachersItems = new SelectList(_teacherRepository.GetNoPairTeachersWithSelected(selectedValue.Value).AsNoTracking(),
											  "TeacherId", "FullName", selectedValue);
			else
				teachersItems = new SelectList(_teacherRepository.NoPairTeachers.AsNoTracking(), "TeacherId", "FullName");

			ViewBag.Teachers = teachersItems;
		}

		[HttpGet]
		public ActionResult Create()
		{
			var model = new AccountViewModel();
			using (var manager = RoleManager)
				model.Roles = manager.Roles.Select(r => new RoleItem() { Value = r.Name })
										   .OrderBy(r => r.Value)
										   .AsNoTracking()
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
					var result = await manager.PasswordValidator.ValidateAsync(model.Password);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.AddModelError("Password", error);

						InitTeachersItems(model.PairTeacherId);

						return View("CreateAccount", model);
					}

					result = await manager.CreateAsync(user, model.Password);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.AddModelError("", error);

						InitTeachersItems(model.PairTeacherId);

						return View("CreateAccount", model);
					}

					if (model.PairTeacherId.HasValue)
						_teacherRepository.AddPairToUser(model.PairTeacherId.Value, user.Id);

					(await manager.AddToRolesAsync(user.Id, model.SelectedRoles.ToArray()))
						.AddErrorsIfFailed(TempData);

					TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.UserCreatedMsg, user.UserName));
				}

				_logger.Info("User \"{0}\" created by {1}.", user.UserName, User.Identity.Name);

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
					TempData.AddMessage(MessageType.Warning, string.Format(GlobalRes.UserNoFound, Id));
					return RedirectToAction(nameof(Table));
				}

				using (var roleManager = RoleManager)
				{
					var roles = roleManager.Roles.AsNoTracking().AsEnumerable();

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
						TempData.AddMessage(MessageType.Danger, string.Format(GlobalRes.UserNoFound, model.Id));
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

					(await manager.RemoveFromRolesAsync(model.Id, userRoles.Intersect(model.UnselectedRoles).ToArray()))
						.AddErrorsIfFailed(TempData);

					(await manager.AddToRolesAsync(model.Id, model.SelectedRoles.Except(userRoles).ToArray()))
						.AddErrorsIfFailed(TempData);

					var pairTeacherIdHasValue = model.PairTeacherId.HasValue;

					if (user.HasTeacher)
						if (pairTeacherIdHasValue)
						{
							if (user.Teacher.TeacherId != model.PairTeacherId.Value)
							{
								// есть учитель у не обновл. пользователя когда есть у обновленого и это не один и тот же учитель
								_teacherRepository.RemovePairToUser(user.Teacher.TeacherId);
								_teacherRepository.AddPairToUser(model.PairTeacherId.Value, user.Id);
							}
						} else
							// есть учитель у не обновл. пользователя когда нет у обновленого
							_teacherRepository.RemovePairToUser(user.Teacher.TeacherId);

					else if (pairTeacherIdHasValue)
						// есть учитель у обновл. пользователя когда нету учителя у не обновленого пользователя
						_teacherRepository.AddPairToUser(model.PairTeacherId.Value, user.Id);

					TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.UserSavedMsg, user.UserName));
					_logger.Info("User \"{0}\" saved by {1}.", user.UserName, User.Identity.Name);
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
			using (var userManager = UserManager)
			{
				var user = await userManager.FindByIdAsync(Id);

				if (user == null)
				{
					TempData.AddMessage(MessageType.Warning, string.Format(GlobalRes.UserNoFound, Id));
					return RedirectToAction(nameof(Table));
				}

				if (await userManager.IsInRoleAsync(user.Id, "Admin"))
					using (var roleManager = RoleManager)
						if ((await roleManager.FindByNameAsync("Admin")).Users.Count() < 2)
						{
							TempData.AddMessage(MessageType.Warning, GlobalRes.CanNotDeleteOnlyAdminUser);
							return RedirectToAction(nameof(Table));
						}
						else if (user.Id == User.Identity.GetUserId<int>())
						{
							TempData.AddMessage(MessageType.Warning, GlobalRes.CanNotDeleteHimSelfHere);
							return RedirectToAction(nameof(Table));
						}

				if (user.HasTeacher)
					_teacherRepository.RemovePairToUser(user.Teacher.TeacherId);

				var result = await userManager.DeleteAsync(user);

				if (result.Succeeded)
					TempData.AddMessage(MessageType.Success, string.Format(GlobalRes.UserDeletedMsg, user.UserName));
				else
					TempData.AddMessages(MessageType.Warning, result.Errors);

				_logger.Info("User \"{0}\" deleted by {1}.", user.UserName, User.Identity.Name);
			}

			return RedirectToAction(nameof(Table));
		}

		private bool disposedValue = false; // To detect redundant calls

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_teacherRepository.Dispose();
					base.Dispose(disposing);
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
	}
}
