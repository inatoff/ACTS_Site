using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class Post
	{
		[HiddenInput(DisplayValue = false)]
		public int PostId { get; set; }

		[Required(ErrorMessage = "Please enter a title")]
		[StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Create { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Content { get; set; }
	}
}