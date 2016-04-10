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

        public IList<string> Tags
        {
            get { return _tags; }
            set { _tags = value; } 
        }

        public string CombinedTags
        {
            get { return string.Join(",", _tags); }
            set
            {
                _tags = value.Split(',').Select(s => s.Trim()).ToList();
            }
        }

        public virtual Blog Blog { get; set; }

        private IList<string> _tags = new List<string>();
    }
}