using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Services
{
	public static class EmailBodyServiceFactory
	{
		private static string _pathToTemplates;

		static EmailBodyServiceFactory()
		{
			var config = new TemplateServiceConfiguration();
			// .. configure Razor
#if DEBUG
			config.Debug = true;
#endif
			Engine.Razor = RazorEngineService.Create(config);
		}

		public static string PathToTemplates
		{
			get { return _pathToTemplates ?? (_pathToTemplates = ConfigurationManager.AppSettings["EmailTemplatesFolder"]); }
			set { _pathToTemplates = value; }
		}

		public static async Task<string> GetEmailBody<T>(T model, string pathToTemplates, string emailType)
		{
			var razor = Engine.Razor;

			if (razor.IsTemplateCached(emailType, null))
				return razor.Run(emailType, null, model);
			else
			{
				string filePath = Path.Combine(pathToTemplates, $"{emailType}.cshtml");
				string templateSource = await filePath.ReadTemplateContent();

				return razor.RunCompile(templateSource, emailType, null, model);
			}
		}

		public static async Task<string> GetEmailBody<T>(T model, string emailType)
		{
			return await GetEmailBody(model, PathToTemplates, emailType);
		}

		static async Task<string> ReadTemplateContent(this string path)
		{
			string content;
			using (var reader = new StreamReader(path))
				content = await reader.ReadToEndAsync();

			return content;
		}
	}
}
