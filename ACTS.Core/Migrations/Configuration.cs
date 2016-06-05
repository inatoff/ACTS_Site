//#define RustamPC
namespace ACTS.Core.Migrations
{
	using Concrete;
	using Entities;
	using Identity;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Collections.Generic;
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
#if RustamPC
			var pathToImagesFolder = @"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\";
#else
			var pathToImagesFolder = @"D:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed";
#endif

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
				var admin = new ApplicationUser
				{
					Email = "rukpet@bigmir.net",
					UserName = "admin",
					LockoutEnabled = true
				};
				userManager.Create(admin, "SuperP@ss");

				userManager.AddToRole(admin.Id, "Admin");

				var teacher = new ApplicationUser
				{
					Email = "teacher@bigmir.net",
					UserName = "teacher",
					LockoutEnabled = true
				};
				userManager.Create(teacher, "SuperP@ss");

				userManager.AddToRole(teacher.Id, "Teacher");
			}

			using (var fileRepository = new FileRepository())
			{
				using (var teacherRepository = new EFTeacherRepository())
					if (!teacherRepository.Teachers.Any())
						new List<Teacher>
					{
						new Teacher
						{
							FullName = "Теленик Сергій Федорович",
							Degree = "д.т.н., професор",
							NameSlug = "telenik",
							Rank = Rank.Head,
							Position = "ЗАВІДУВАЧ КАФЕДРИ голова НМК МОН України \"Автоматика та управління\" керівник циклу математичних дисциплін",
							PhotoId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "Теленик.jpg")),
							Blog = new Blog()
						},

						new Teacher
						{
							FullName = "Новацький Анатолій Олександрович",
							Degree = "к.т.н., доцент",
							NameSlug = "novackiy",
							Rank = Rank.FirstVice,
							Position = "ПЕРШИЙ ЗАСТУПНИК заступник з навчально-виховної роботи відповідальний за заочну форму навчання керівник циклу \"Комп'ютерна електроніка та мікропроцесорна техніка\"",
							PhotoId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "Новацкий.jpg")),
							Blog = new Blog()
						}
					}.ForEach(t => teacherRepository.CreateTeacher(t));

				using (var eventRepository = new EFEventRepository())
					if (!eventRepository.Events.Any())
						new List<Event>
					{
						new Event
						{
							StartView = DateTime.UtcNow.AddDays(-10d),
							EndView = DateTime.UtcNow.AddDays(10d),
							Content = "<h1>Проект Умов прийому до ВНЗ</h1>"
									+ "<p>Міністерством освіти і науки розроблено Умови прийому до вищих навчальних закладів України в 2016 році. Текст проекту документа у своєму Facebook оприлюднив міністр освіти і науки Сергій Квіт.</p>",
							Title = "Вступнику 2016-го",
							UrlSlug = "vstupniku-2016-go",
							ImageId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "testImage.jpg"))
						},
						new Event
						{
							StartView = DateTime.UtcNow.AddDays(-10d),
							EndView = DateTime.UtcNow.AddDays(10d),
							Content = "<h1>Проект Умов прийому до ВНЗ</h1>"
									+ "<p>Міністерством освіти і науки розроблено Умови прийому до вищих навчальних закладів України в 2016 році. Текст проекту документа у своєму Facebook оприлюднив міністр освіти і науки Сергій Квіт.</p>",
							Title = "Вступнику 2016-го",
							UrlSlug = "vstupniku-2016-go",
							ImageId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "testImage.jpg"))
						},
						new Event
						{
							StartView = DateTime.UtcNow.AddDays(-1d),
							EndView = DateTime.UtcNow.AddDays(1d),
							Content = "<h3>Test event3</h3>",
							Title = "Test title3",
							UrlSlug = "test-title3",
							ImageId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "testImage.jpg"))
						}
					}.ForEach(e => eventRepository.CreateEvent(e));



				using (var employeeRepository = new EFEmployeeRepository())
					if (!employeeRepository.Employees.Any())
						new List<Employee>
					{
						new Employee
						{
							FullName = "Щербань Олександр Васильович",
							Position = "Завідувач лабораторіями",
							PhotoId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "sherban.jpg"))
						},
						new Employee
						{
							FullName = "Власова Тетяна Григоріївна",
							Position = "Старший лаборант",
							PhotoId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "vlasova.jpg"))
						},
						new Employee
						{
							FullName = "Огородник Ірина Віталіївна",
							Position = "Провідний інженер",
							PhotoId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "ohorodnik.jpg"))
						}
					}.ForEach(e => employeeRepository.CreateEmployee(e));

				using (var newsRepository = new EFNewsRepository())
					if (!newsRepository.Uncos.Any())
						new List<News>
					{
						new News
						{
							Title = "Увага студенти магістри 6 курсу!",
							Content = "<p>Студентам магістрам 6 курсу необхідно<span style=\"color: #ff0000;\"> до 15 травня 2014 р.</span> на своїх кафедрах перевірити випускні оцінки, розписатися у випускних документах та здати в деканат копію ідентифікаційного кода.</p>",
							ImageId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "magisters.jpg"))
						},
						new News
						{
							Title = "Трансляція в Periscope",
							Content = "<h3>Онлайн трансляція урочистого відкриття конференції у Periscope за нашим twitter акаунтом @icacit15</h3><p> &nbsp;</p>   <p> За допомогою браузера комп &#8217;ютера <a href=\"https://www.periscope.tv/icacit15\" target=\"_blank\">https://www.periscope.tv/icacit15</a></p><p> &nbsp;</p>   <p> Завантажити додаток для Android <a href=\"https://play.google.com/store/apps/details?id=tv.periscope.android\" target = \"_blank\" > https://play.google.com/store/apps/details?id=tv.periscope.android</a></p><p> Завантажити додаток для iOS <a href=\"https://itunes.apple.com/us/app/id972909677?mt=8\" target=\"_blank\"> https://itunes.apple.com/us/app/id972909677?mt=8</a></p>",
							ImageId = fileRepository.CreateFile(Path.Combine(pathToImagesFolder, "periscope.jpg"))
						},
						new News
						{
							Title = "Department ACTS a premier partner Cisco Networking Academy",
							Content = "<p><a href=\"http://acts.kpi.ua/app/uploads/2016/05/20160524142324.jpg\"><img src=\"http://acts.kpi.ua/app/uploads/2016/05/20160524142324.jpg\" alt=\"20160524142324\" width=\"3264\" height=\"2448\" class=\"alignnone size-full wp-image-2299\"></a></p>"
						},
						new News
						{
							Title = "Графік днів відкритих дверей",
							Content = "<p><a href=\"http://acts.kpi.ua/app/uploads/2016/03/Untitled1.jpg\"><img class=\"alignnone size-full wp-image-2188\" src=\"http://acts.kpi.ua/app/uploads/2016/03/Untitled1.jpg\" alt=\"Untitled\" width=\"922\" height=\"1305\"></a></p>",
						}
					}.ForEach(n => newsRepository.CreateNews(n));
			}

			base.Seed(context);
		}
	}
}
		
