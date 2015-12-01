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
		public int NewsID { get; set; }

		public string Title { get; set; }

		public DateTime? DataCreate { get; set; }

		public byte [] ImageData { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; } 
	}
}
