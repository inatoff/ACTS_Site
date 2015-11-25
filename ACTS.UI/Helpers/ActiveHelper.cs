using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Helpers
{
	public static class ActiveHelper
	{
		public static string Active(this string culture, string currentCulture)
		{
			return culture == currentCulture ? "selectedLanguage" : string.Empty;
		}

		//public static string Active(string culture, string currentCulture)
		//{
		//	return culture == currentCulture ? "selectedLanguage" : string.Empty;
		//}
	}
}
