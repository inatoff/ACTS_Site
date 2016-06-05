using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Ninject.Extensions.Logging;
using Ninject;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(ACTS.UI.Startup))]

namespace ACTS.UI
{
	public partial class Startup
	{
		private static ILogger _logger;

		protected static ILogger Logger
		{
			get
			{
				return _logger ?? (_logger = DependencyResolver.Current.GetService<ILoggerFactory>().GetCurrentClassLogger());
			}
		}

		public void Configuration(IAppBuilder app)
		{
			// Дополнительные сведения о настройке приложения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=316888
			Logger.Trace("Owin start configuring.");

			ConfigureAuth(app);

			Logger.Trace("Owin end configuring.");
		}
	}
}
