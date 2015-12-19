using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class Post
	{
		[Key]
		[HiddenInput(DisplayValue = false)]
		public int PostID { get; set; }

		[Required(ErrorMessage = "Please enter a title")]
		[StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Create { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		[Required(ErrorMessage = "Please enter a description")]
		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Content { get; set; }
	}
}