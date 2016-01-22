using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Helpers
{
	public static class DescriptionHelper
	{
		/// <summary>
		/// Returns a description for this model property
		/// </summary>
		public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			return MvcHtmlString.Create(string.Format(metadata.Description));
		}
	}
}
