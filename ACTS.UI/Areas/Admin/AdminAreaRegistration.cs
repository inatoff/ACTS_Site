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
			//context.MapRoute(
			//	name: "ToDefaultAdminAction",
			//	url: "Admin/{controller}/{action}",
			//	defaults: new { controller = "Admin", action = "Index" }
			//);

			context.MapRoute(
				name: "Admin_default",
				url:  "Admin/{controller}/{action}/{id}",
				defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}