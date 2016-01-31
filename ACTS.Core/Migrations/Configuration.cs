#define RustamPC
namespace ACTS.Core.Migrations
{
	using Concrete;
	using Entities;
	using Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ACTS.Core.Concrete.EFDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ACTS.Core.Concrete.EFDbContext context)
		{
			var roleStore = new ApplicationRoleStore(context);
			var roleManager = new RoleManager<ApplicationRole, int>(roleStore);

			if (!roleManager.RoleExists("Admin"))
			{
				var role = new ApplicationRole("Admin");
				roleManager.Create(role);
			}

			if (!roleManager.RoleExists("Teacher"))
			{
				var role = new ApplicationRole("Teacher");
				roleManager.Create(role);
			}

			var userStrore = new ApplicationUserStore(context);
			var userManager = new UserManager<ApplicationUser, int>(userStrore);

			if (!userManager.Users.Any())
			{
				var admin = new ApplicationUser {
					Email = "rukpet@bigmir.net",
					UserName = "admin",
					LockoutEnabled = true
				}; 
				userManager.Create(admin, "SuperP@ss"); 

				userManager.AddToRole(admin.Id, "Admin");

				var teacher = new ApplicationUser {
					Email = "teacher@bigmir.net",
					UserName = "teacher",
					LockoutEnabled = true
				};
				userManager.Create(teacher, "SuperP@ss");

				userManager.AddToRole(teacher.Id, "Teacher");
			}

			var teacherRepository = new EFTeacherRepository();


			if (!teacherRepository.Teachers.Any())
			{
#if RustamPC
				using (var fs = new FileStream(@"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\Теленик.jpg", FileMode.Open))
#else
				using (var fs = new FileStream(@"E:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed\Теленик.jpg", FileMode.Open))
#endif
                {
                    var teacher = new Teacher()
                    {
                        FullName = "Теленик Сергій Федорович",
                        Degree = "д.т.н., професор",
                        Rank = Rank.Head,
                        Position = "ЗАВІДУВАЧ КАФЕДРИ голова НМК МОН України \"Автоматика та управління\" керівник циклу математичних дисциплін",
                        Photo = new byte[fs.Length],
                        PhotoMimeType = "jpg"
                    };
                    fs.Read(teacher.Photo, 0, (int)fs.Length);
                    teacher.PhotoMimeType = "jpg";

					teacherRepository.CreateTeacher(teacher);
				}
#if RustamPC
				using (var fs = new FileStream(@"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\Новацкий.jpg", FileMode.Open))
#else
				using (var fs = new FileStream(@"E:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed\Новацкий.jpg", FileMode.Open))
#endif
                {
                    var teacher = new Teacher()
                    {
                        FullName = "Новацький Анатолій Олександрович",
                        Degree = "к.т.н., доцент",
                        Rank = Rank.FirstVice,
                        Position = "ПЕРШИЙ ЗАСТУПНИК заступник з навчально-виховної роботи відповідальний за заочну форму навчання керівник циклу \"Комп'ютерна електроніка та мікропроцесорна техніка\"",
                        Photo = new byte[fs.Length],
                        PhotoMimeType = "jpg"
                    };
                    fs.Read(teacher.Photo, 0, (int)fs.Length);
                    teacher.PhotoMimeType = "jpg";

					teacherRepository.CreateTeacher(teacher);
				}
			}

			base.Seed(context);
		}
	}
}
		
