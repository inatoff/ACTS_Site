using ACTS.UI.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.Controllers
{
	public class HomeController : BaseController
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult SetCulture(string culture, string id)
		{
			// Validate input
			culture = CultureHelper.GetImplementedCulture(culture);

			RouteData.Values ["culture"] = culture;  // set culture

			return RedirectToAction(string.IsNullOrEmpty(id) ? "Index" : id);
		}   

		public ActionResult About()
		{
			return View();
		}
	}
}