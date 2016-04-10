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
	public class Event
	{
		[HiddenInput(DisplayValue = false)]
		public int EventId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Created { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? Modified { get; set; }

		/// <summary>
		/// Время с которого начинается показоваться событие в UTC формате на сайте.
		/// </summary>
		public DateTime? StartView { get; set; }

		/// <summary>
		/// Время когда событие перестает показоваться в UTC формате на сайте.
		/// </summary>
		public DateTime? EndView { get; set; }

		[Display(Name = nameof(DisplayRes.SlugName), ResourceType = typeof(DisplayRes))]
		public string UrlSlug { get; set; }

		[CustomRequired]
		[CustomMaxLength(500)]
		[Display(Name = nameof(DisplayRes.TitleName), ResourceType = typeof(DisplayRes))]
		public string Title { get; set; }

		[Display(Name = nameof(DisplayRes.ImageName), ResourceType = typeof(DisplayRes))]
		public byte[] ImageData { get; set; }

		[HiddenInput]
		public string ImageMimeType { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		[Display(Name = nameof(DisplayRes.ContentName), ResourceType = typeof(DisplayRes))]
		public string Content { get; set; }

		public bool IsActive {
			get
			{
				var utcNow = DateTime.UtcNow;
				return StartView.HasValue && EndView.HasValue ? StartView.Value < utcNow && EndView.Value > utcNow : false;
			}
		}

	}
}
