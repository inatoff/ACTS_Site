using ACTS.Localization.Resources;
using ACTS.UI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class ResourcesController : BaseController
	{
		// GET: Resources/Required
		public ActionResult Required()
		{
			var res = RequiredRes.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
												 .Cast<DictionaryEntry>();

			var defaultCulture = new CultureInfo(CultureHelper.GetDefaultCulture());
			if (CultureInfo.CurrentUICulture != defaultCulture)
			{
				var defaultRes = RequiredRes.ResourceManager.GetResourceSet(defaultCulture, true, true)
														    .Cast<DictionaryEntry>();

				res = Enumerable.Union(res, defaultRes, new DictionaryEntryEqualityComparer());
			}

			var script = res.Aggregate(new StringBuilder("var requiredRes = {"),
							(sb, de) => sb.AppendFormat("{0}:\"{1}\",", de.Key, HttpUtility.JavaScriptStringEncode((string)de.Value)),
							sb => sb.Remove(sb.Length - 1, 1).Append("};").ToString());

			return JavaScript(script);
		}

		public ActionResult Display()
		{
			var res = DisplayRes.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
												.Cast<DictionaryEntry>();

			var defaultCulture = new CultureInfo(CultureHelper.GetDefaultCulture());
			if (CultureInfo.CurrentUICulture != defaultCulture)
			{
				var defaultRes = DisplayRes.ResourceManager.GetResourceSet(defaultCulture, true, true)
														   .Cast<DictionaryEntry>();

				res = Enumerable.Union(res, defaultRes, new DictionaryEntryEqualityComparer());
			}

			var script = res.Aggregate(new StringBuilder("var displayRes = {"),
							(sb, de) => sb.AppendFormat("{0}:\"{1}\",", de.Key, HttpUtility.JavaScriptStringEncode((string)de.Value)),
							sb => sb.Remove(sb.Length - 1, 1).Append("};").ToString());

			return JavaScript(script);
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