using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ACTS.UI.Infrastructure
{
	/// <summary>
	/// get from http://stackoverflow.com/questions/2504923/how-to-redirect-authorize-to-loginurl-only-when-roles-are-not-used
	/// </summary>
	class SecurityAttribute : AuthorizeAttribute
	{
		private string _redirectUrl = "/Error/Unauthorized";

		public string RedirectUrl
		{
			get { return _redirectUrl; }
			set { _redirectUrl = value; }
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}

			if (AuthorizeCore(filterContext.HttpContext))
			{
				HttpCachePolicyBase cachePolicy =
					filterContext.HttpContext.Response.Cache;
				cachePolicy.SetProxyMaxAge(new TimeSpan(0));
				cachePolicy.AddValidationCallback(CacheValidateHandler, null);
			}

			/// This code added to support custom Unauthorized pages.
			else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				if (RedirectUrl != null)
					filterContext.Result = new RedirectResult(RedirectUrl);
				else
					// Redirect to Login page.
					HandleUnauthorizedRequest(filterContext);
			}
			/// End of additional code
			else
			{
				// Redirect to Login page.
				HandleUnauthorizedRequest(filterContext);
			}

		}

		protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
		{
			validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
		}
	}
}
