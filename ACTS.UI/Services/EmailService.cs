using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Services
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
}
