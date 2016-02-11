﻿using System;
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
		[HiddenInput(DisplayValue = false)]
		public int NewsId { get; set; }

		[Required(AllowEmptyStrings = false)]
		[StringLength(500, ErrorMessage = "Title: Length should not exceed {1} characters")]
		[Display(Name = "Title*")]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Created { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		[Display(Name = "Image")]
		public byte [] ImageData { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Content { get; set; }
	}
}
