using ACTS.UI.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.UI.Controllers
{
	public class HomeController : BaseController
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult SetCulture(string culture, string returnUrl)
		{
			// Validate input
			culture = CultureHelper.GetImplementedCulture(culture);
			// Save culture in a cookie
			HttpCookie cookie = Request.Cookies ["_culture"];
			if (cookie != null)
				cookie.Value = culture;   // update cookie value
			else
			{
				cookie = new HttpCookie("_culture");
				cookie.Value = culture;
				cookie.Expires = DateTime.Now.AddYears(1);
			}
			Response.Cookies.Add(cookie);
			if (!string.IsNullOrEmpty(returnUrl)) 
				return Redirect(returnUrl);
			return RedirectToAction("Index");
		}   
        public ActionResult Contacts()
        {
            return View();
        }
	}
}