using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Infrastructure
{
	public static class EmailBodyFactory
	{
		static EmailBodyFactory()
		{
			var config = new TemplateServiceConfiguration();
			// .. configure your instance
#if DEBUG
			config.Debug = true;
#endif
			Engine.Razor = RazorEngineService.Create(config);
		}

		public static string DefaultPathToTemplates { get; set; }

		public static string GetEmailBody<T>(T model, string pathToTemplates, string emailType)
		{
			var razor = Engine.Razor;

			if (razor.IsTemplateCached(emailType, null))
				return razor.Run(emailType, null, model);
			else
			{
				string filePath = Path.Combine(pathToTemplates, $"{emailType}.cshtml");
				string templateSource = filePath.ReadTemplateContent();

				return razor.RunCompile(templateSource, emailType, null, model);
			}
		}

		public static string GetEmailBody<T>(T model, string emailType)
		{
			var razor = Engine.Razor;

			if (razor.IsTemplateCached(emailType, null))
				return razor.Run(emailType, null, model);
			else
			{
				string filePath = Path.Combine(DefaultPathToTemplates, $"{emailType}.cshtml");
				string templateSource = filePath.ReadTemplateContent();

				return razor.RunCompile(templateSource, emailType, null, model);
			}
		}

		static string ReadTemplateContent(this string path)
		{
			string content;
			using (var reader = new StreamReader(path))
				content = reader.ReadToEnd();

			return content;
		}
	}
}
