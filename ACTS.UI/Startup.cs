using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ACTS.UI.Startup))]

namespace ACTS.UI
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Дополнительные сведения о настройке приложения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=316888
			ConfigureAuth(app);
		}
	}
}
