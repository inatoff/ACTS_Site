using ACTS.Core.Abstract;
using ACTS.Localization;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class News : IHaveFileId
	{
		[HiddenInput(DisplayValue = false)]
		public int NewsId { get; set; }

		[CustomRequired]
		[CustomMaxLength(500)]
		[Display(Name = nameof(DisplayRes.TitleName), ResourceType = typeof(DisplayRes))]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Created { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		[Display(Name = nameof(DisplayRes.ImageName), ResourceType = typeof(DisplayRes))]
		public Guid? ImageId { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		[Display(Name = nameof(DisplayRes.ContentName), ResourceType = typeof(DisplayRes))]
		public string Content { get; set; }

		Guid? IHaveFileId.FileId { get { return ImageId; } set { ImageId = value; } } 
	}
}
