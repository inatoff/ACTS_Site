using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ACTS.Core.Concrete;
using ACTS.Core.Identity;
using System.Net.Mail;
using System.Net;
using System.Net.Configuration;
using System.Configuration;

namespace ACTS.UI
{
	public class EmailService : IIdentityMessageService
	{
		public async Task SendAsync(IdentityMessage message)
		{
			var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			string fromAddress = smtpSection.From;

			using (var smtp = new SmtpClient())
			using (var mailMessage = new MailMessage(fromAddress, message.Destination)
			{
				Subject = message.Subject,
				Body = message.Body,
				IsBodyHtml = true
			})
			{
				await new SmtpClient().SendMailAsync(mailMessage);
			}
		}
	}

	// Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
	public class ApplicationUserManager : UserManager<ApplicationUser, int>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
			:base(store)
		{
			// Настройка логики проверки имен пользователей
			this.UserValidator = new UserValidator<ApplicationUser, int>(this) {
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true,
			};

			// Настройка логики проверки паролей
			this.PasswordValidator = new PasswordValidator {
				RequiredLength = 8,
				RequireNonLetterOrDigit = false,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true
			};

			// Настройка параметров блокировки по умолчанию
			this.UserLockoutEnabledByDefault = true;
			this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(1);
			this.MaxFailedAccessAttemptsBeforeLockout = 3;

			this.EmailService = new EmailService();
		}

		public ApplicationUserManager(EFDbContext context)
			:base(new ApplicationUserStore(context))
		{
		}

		public ApplicationUserManager()
			:base(new ApplicationUserStore(new EFDbContext()))
		{
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
		{
			var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<EFDbContext>()));

			// Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
			// Здесь можно указать собственный поставщик и подключить его.
			//manager.RegisterTwoFactorProvider("Код, полученный по телефону", new PhoneNumberTokenProvider<IdentityUser>
			//{
			//	MessageFormat = "Ваш код безопасности: {0}"
			//});
			//manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<IdentityUser>
			//{
			//	Subject = "Код безопасности",
			//	BodyFormat = "Ваш код безопасности: {0}"
			//});
			
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				var tokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity", "ACTS.WebSite"));
				tokenProvider.TokenLifespan = TimeSpan.FromHours(6.0);
				manager.UserTokenProvider = tokenProvider;
			}
			return manager;
		}
	}

	// Настройка диспетчера входа для приложения.
	public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
	{
		ApplicationSignInManager(UserManager<ApplicationUser, int> userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}

		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
		{
			return new ApplicationSignInManager
				(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
		}
	}

	public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
	{
		public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
			: base(store)
		{
		}

		public ApplicationRoleManager(EFDbContext context)
			:base(new ApplicationRoleStore(context))
		{
		}

		public ApplicationRoleManager()
			:base(new ApplicationRoleStore(new EFDbContext()))
		{
		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
		{
			return new ApplicationRoleManager(new ApplicationRoleStore(context.Get<EFDbContext>()));
		}
	}
}
