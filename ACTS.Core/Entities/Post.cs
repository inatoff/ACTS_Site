using ACTS.Localization;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class Post
	{
		[HiddenInput(DisplayValue = false)]
		public int PostId { get; set; }

		[CustomRequired]
		[CustomMaxLength(500)]
		[Display(Name = nameof(DisplayRes.TitleName), ResourceType = typeof(DisplayRes))]
		public string Title { get; set; }

		[Display(Name = nameof(DisplayRes.SlugName), ResourceType = typeof(DisplayRes))]
		public string Slug { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Created { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Content { get; set; }

		public virtual IList<Tag> Tags { get; set; } = new List<Tag>();

		public virtual Blog Blog { get; set; }

	}
}