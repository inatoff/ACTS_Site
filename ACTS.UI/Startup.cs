using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Ninject.Extensions.Logging;
using Ninject;

[assembly: OwinStartup(typeof(ACTS.UI.Startup))]

namespace ACTS.UI
{
	public partial class Startup
	{
		private static ILogger _logger;
		static Startup()
		{
			IKernel kernel = new StandardKernel();
				_logger = kernel.Get<ILoggerFactory>().GetCurrentClassLogger();
		}

		public void Configuration(IAppBuilder app)
		{
			// Дополнительные сведения о настройке приложения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=316888
			_logger.Trace("Owin start configuring.");

			ConfigureAuth(app);

			_logger.Trace("Owin end configuring.");
		}
	}
}
