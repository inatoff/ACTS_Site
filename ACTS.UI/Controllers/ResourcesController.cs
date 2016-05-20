using ACTS.Localization.Resources;
using ACTS.UI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class ResourcesController : BaseController
	{
		// GET: Resources
		public ActionResult Required()
		{
			Response.ContentType = "text/javascript";
			var res = RequiredRes.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
												 .Cast<DictionaryEntry>();

			var defaultCulture = new CultureInfo(CultureHelper.GetDefaultCulture());
			if (CultureInfo.CurrentUICulture != defaultCulture)
			{
				var defaultRes = RequiredRes.ResourceManager.GetResourceSet(defaultCulture, true, true)
														    .Cast<DictionaryEntry>();

				res = Enumerable.Union(res, defaultRes, new DictionaryEntryEqualityComparer());
			}

			return View(res);
		}

		public ActionResult Display()
		{
			Response.ContentType = "text/javascript";
			var res = DisplayRes.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
												.Cast<DictionaryEntry>();

			var defaultCulture = new CultureInfo(CultureHelper.GetDefaultCulture());
			if (CultureInfo.CurrentUICulture != defaultCulture)
			{
				var defaultRes = DisplayRes.ResourceManager.GetResourceSet(defaultCulture, true, true)
														   .Cast<DictionaryEntry>();

				res = Enumerable.Union(res, defaultRes, new DictionaryEntryEqualityComparer());
			}

			return View(res);
		}

		public class DictionaryEntryEqualityComparer : EqualityComparer<DictionaryEntry>
		{
			public override bool Equals(DictionaryEntry x, DictionaryEntry y)
			{
				return Object.Equals(x.Key, y.Key);
			}

			public override int GetHashCode(DictionaryEntry obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}