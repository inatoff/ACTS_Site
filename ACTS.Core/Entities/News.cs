using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class News
	{
		[Key]
		[HiddenInput(DisplayValue = false)]
		public int NewsID { get; set; }

		[Required(ErrorMessage = "Please enter a title")]
		[StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Create { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		public byte [] ImageData { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; }

		[Required(ErrorMessage = "Please enter a description")]
		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Content { get; set; }
	}
}
