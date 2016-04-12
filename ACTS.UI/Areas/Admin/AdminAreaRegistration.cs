using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration 
	{
		public override string AreaName 
		{
			get 
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) 
		{
			context.MapRoute(
				name: "ToDefaultAdminAreaAction",
				url: "Admin",
				defaults: new { controller = "Admin", action = "Dashboard" }
			);

			context.MapRoute(
				name: "ToDefaultAdminAction",
				url: "Admin/Dashboard",
				defaults: new { controller = "Admin", action = "Dashboard" }
			);

			context.MapRoute(
				name: "ToLogsRssFeed",
				url: "Admin/Logging/Rss",
				defaults: new { controller = "Logging", action = "Rss" }
			);

			context.MapRoute(
				name: "Admin_default",
				url:  "Admin/{controller}/{action}/{id}",
				defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}