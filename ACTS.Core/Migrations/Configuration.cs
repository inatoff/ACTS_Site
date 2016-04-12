//#define RustamPC
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
#if RustamPC
			var pathToImagesFolder = @"D:\ACTS\ACTS_Site\ACTS.Core\Migrations\ImagesForSeed\";
#else
			var pathToImagesFolder = @"E:\study\ACTS\ACTS.Core\Migrations\ImagesForSeed\";
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
			const string mimeType = "jpg";

			if (!teacherRepository.Teachers.Any())
			{
				using (var fs = new FileStream(Path.Combine(pathToImagesFolder, "�������.jpg"), FileMode.Open))
				{
					var teacher = new Teacher()
					{
						FullName = "������� ����� ���������",
						Degree = "�.�.�., ��������",
						NameSlug = "telenik",
						Rank = Rank.Head,
						Position = "��²����� ������� ������ ��� ��� ������ \"���������� �� ���������\" ������� ����� ������������ ��������",
						Photo = new byte[fs.Length],
						PhotoMimeType = mimeType,
						Blog = new Blog()
					};
					fs.Read(teacher.Photo, 0, (int)fs.Length);

					teacherRepository.CreateTeacher(teacher);
				}

				using (var fs = new FileStream(Path.Combine(pathToImagesFolder, "��������.jpg"), FileMode.Open))
				{
					var teacher = new Teacher()
					{
						FullName = "��������� ������� �������������",
						Degree = "�.�.�., ������",
						NameSlug = "novackiy",
						Rank = Rank.FirstVice,
						Position = "������ ��������� ��������� � ���������-������� ������ ������������ �� ������ ����� �������� ������� ����� \"����'������ ���������� �� �������������� ������\"",
						Photo = new byte[fs.Length],
						PhotoMimeType = mimeType,
						Blog = new Blog()
					};
					fs.Read(teacher.Photo, 0, (int)fs.Length);

					teacherRepository.CreateTeacher(teacher);
				}
			}

			var eventRepository = new EFEventRepository();

			if (!eventRepository.Events.Any())
			{
				using (FileStream testImageFs = new FileStream(Path.Combine(pathToImagesFolder, "testImage.jpg"), FileMode.Open),
					   maxresdefaultFs = new FileStream(Path.Combine(pathToImagesFolder, "maxresdefault.jpg"), FileMode.Open))
				{
					var image = new byte[testImageFs.Length];
					testImageFs.Read(image, 0, (int)testImageFs.Length);

					var maxresdefault = new byte[maxresdefaultFs.Length];
					maxresdefaultFs.Read(maxresdefault, 0, (int)maxresdefaultFs.Length);

					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-10d),
						EndView = DateTime.UtcNow.AddDays(10d),
						Content = "<h1>������ ���� ������� �� ���</h1>"
								+ "<p>̳���������� ����� � ����� ���������� ����� ������� �� ����� ���������� ������� ������ � 2016 ����. ����� ������� ��������� � ����� Facebook ���������� ����� ����� � ����� ����� ���.</p>",
						Title = "��������� 2016-��",
						UrlSlug = "vstupniku-2016-go",
						ImageData = maxresdefault,
						ImageMimeType = mimeType
					});

					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-10d),
						EndView = DateTime.UtcNow,
						Content = "<h2>Test event2</h2>",
						Title = "Test title2",
						UrlSlug = "test-title2",
						ImageData = image,
						ImageMimeType = mimeType
					});


					eventRepository.CreateEvent(new Entities.Event
					{
						StartView = DateTime.UtcNow.AddDays(-1d),
						EndView = DateTime.UtcNow.AddDays(1d),
						Content = "<h3>Test event3</h3>",
						Title = "Test title3",
						UrlSlug = "test-title3",
						ImageData = image,
						ImageMimeType = mimeType
					});
				}
			}

			var employeeRepository = new EFEmployeeRepository();

			if (!employeeRepository.Employees.Any())
			{
				using (FileStream sherbanFs = new FileStream(Path.Combine(pathToImagesFolder, "sherban.jpg"), FileMode.Open))
				{
					var sherbanPhoto = new byte[sherbanFs.Length];
					sherbanFs.Read(sherbanPhoto, 0, (int)sherbanFs.Length);

					employeeRepository.CreateEmployee(new Employee {
						FullName = "������� ��������� ����������",
						Position = "�������� ������������",
						Photo = sherbanPhoto,
						PhotoMimeType = mimeType
					});
				}

				using (FileStream vlasovaFs = new FileStream(Path.Combine(pathToImagesFolder, "vlasova.jpg"), FileMode.Open))
				{
					var vlasovaPhoto = new byte[vlasovaFs.Length];
					vlasovaFs.Read(vlasovaPhoto, 0, (int)vlasovaFs.Length);

					employeeRepository.CreateEmployee(new Employee
					{
						FullName = "������� ��������� ����������",
						Position = "�������� ������������",
						Photo = vlasovaPhoto,
						PhotoMimeType = mimeType
					});
				}

				using (FileStream ohorodnikFs = new FileStream(Path.Combine(pathToImagesFolder, "ohorodnik.jpg"), FileMode.Open))
				{
					var ohorodnikPhoto = new byte[ohorodnikFs.Length];
					ohorodnikFs.Read(ohorodnikPhoto, 0, (int)ohorodnikFs.Length);

					employeeRepository.CreateEmployee(new Employee
					{
						FullName = "��������� ����� ³��볿���",
						Position = "�������� �������",
						Photo = ohorodnikPhoto,
						PhotoMimeType = mimeType
					});
				}
			}

			var newsRepository = new EFNewsRepository();

			if (!newsRepository.Uncos.Any())
			{
				using (FileStream magistersFs = new FileStream(Path.Combine(pathToImagesFolder, "magisters.jpg"), FileMode.Open))
				{
					var magistersImage = new byte[magistersFs.Length];
					magistersFs.Read(magistersImage, 0, (int)magistersFs.Length);

					newsRepository.CreateNews(new News
					{
						Title = "����� �������� ������� 6 �����!",
						Content = "<p>��������� �������� 6 ����� ���������<span style=\"color: #ff0000;\"> �� 15 ������ 2014 �.</span> �� ���� �������� ��������� ������� ������, ����������� � ��������� ���������� �� ����� � ������� ���� ����������������� ����.</p>",
						ImageData = magistersImage,
						ImageMimeType = mimeType
					});
				}

				using (FileStream periscopeFs = new FileStream(Path.Combine(pathToImagesFolder, "periscope.jpg"), FileMode.Open))
				{
					var periscopeImage = new byte[periscopeFs.Length];
					periscopeFs.Read(periscopeImage, 0, (int)periscopeFs.Length);

					newsRepository.CreateNews(new News
					{
						Title = "���������� � Periscope",
						Content = "<h3>������ ���������� ���������� �������� ����������� � Periscope �� ����� twitter �������� @icacit15</h3><p> &nbsp;</p>   <p> �� ��������� �������� ���� &#8217;����� <a href=\"https://www.periscope.tv/icacit15\" target=\"_blank\">https://www.periscope.tv/icacit15</a></p><p> &nbsp;</p>   <p> ����������� ������� ��� Android <a href=\"https://play.google.com/store/apps/details?id=tv.periscope.android\" target = \"_blank\" > https://play.google.com/store/apps/details?id=tv.periscope.android</a></p><p> ����������� ������� ��� iOS <a href=\"https://itunes.apple.com/us/app/id972909677?mt=8\" target=\"_blank\"> https://itunes.apple.com/us/app/id972909677?mt=8</a></p>",
						ImageData = periscopeImage,
						ImageMimeType = mimeType
					});
				}
			}

			base.Seed(context);
		}
	}
}
		
