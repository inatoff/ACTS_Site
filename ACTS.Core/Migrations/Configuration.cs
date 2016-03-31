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
						NameSlug = "telenik",
						Rank = Rank.Head,
						Position = "ЗАВІДУВАЧ КАФЕДРИ голова НМК МОН України \"Автоматика та управління\" керівник циклу математичних дисциплін",
						Photo = new byte[fs.Length],
						PhotoMimeType = "jpg",
						Blog = new Blog()
					};
					fs.Read(teacher.Photo, 0, (int)fs.Length);

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
						NameSlug = "novackiy",
						Rank = Rank.FirstVice,
						Position = "ПЕРШИЙ ЗАСТУПНИК заступник з навчально-виховної роботи відповідальний за заочну форму навчання керівник циклу \"Комп'ютерна електроніка та мікропроцесорна техніка\"",
						Photo = new byte[fs.Length],
						PhotoMimeType = "jpg",
						Blog = new Blog()
					};
					fs.Read(teacher.Photo, 0, (int)fs.Length);

					teacherRepository.CreateTeacher(teacher);
				}
			}

			var eventRepository = new EFEventRepository();

			if (!eventRepository.Events.Any())
			{
#if RustamPC
				using (FileStream testImageFs = new FileStream(@"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\testImage.jpg", FileMode.Open),
					   maxresdefaultFs = new FileStream(@"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\maxresdefault.jpg", FileMode.Open))
#else
				using (FileStream testImageFs = new FileStream(@"E:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed\testImage.jpg", FileMode.Open),
					   maxresdefaultFs = new FileStream(@"E:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed\maxresdefault.jpg", FileMode.Open))
#endif
				{
					var image = new byte[testImageFs.Length];
					testImageFs.Read(image, 0, (int)testImageFs.Length);

					var maxresdefault = new byte[maxresdefaultFs.Length];
					maxresdefaultFs.Read(maxresdefault, 0, (int)maxresdefaultFs.Length);

					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-10d),
						EndView = DateTime.UtcNow.AddDays(10d),
						Content = "<h1>Проект Умов прийому до ВНЗ</h1>"
								+ "<p>Міністерством освіти і науки розроблено Умови прийому до вищих навчальних закладів України в 2016 році. Текст проекту документа у своєму Facebook оприлюднив міністр освіти і науки Сергій Квіт.</p>",
						Title = "Вступнику 2016-го",
						UrlSlug = "vstupniku-2016-go",
						ImageData = maxresdefault,
						ImageMimeType = "jpg"
					});

					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-10d),
						EndView = DateTime.UtcNow,
						Content = "<h2>Test event2</h2>",
						Title = "Test title2",
						UrlSlug = "test-title2",
						ImageData = image,
						ImageMimeType = "jpg"
					});


					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-1d),
						EndView = DateTime.UtcNow.AddDays(1d),
						Content = "<h3>Test event3</h3>",
						Title = "Test title3",
						UrlSlug = "test-title3",
						ImageData = image,
						ImageMimeType = "jpg"
					});
				}
			}
			
			base.Seed(context);
		}
	}
}
		
