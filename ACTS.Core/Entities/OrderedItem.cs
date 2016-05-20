using ACTS.Localization;
using ACTS.Localization.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTS.Core.Entities
{
	public class OrderedItem<TValue, TOrder> where TOrder : IComparable
	{
		[Key]
		public int OrderedItemId { get; set; }

		public TOrder Order { get; set; }

		[CustomRequired(AllowEmptyStrings = true)]
		public TValue Value { get; set; }

		public int TeacherId { get; set; }

		[ForeignKey(nameof(TeacherId))]
		public Teacher Teacher { get; set; }
	}
  
	[MetadataType(typeof(DisciplineMetadata))]
	public class Discipline : OrderedItem<string, int> { }

	public class DisciplineMetadata
	{
		[Display(Name = nameof(DisplayRes.DisciplineName), ResourceType = typeof(DisplayRes))]
		public string Value { get; set; }
	}

	[MetadataType(typeof(ScienceInterestMetadata))]
	public class ScienceInterest : OrderedItem<string, int> { }

	public class ScienceInterestMetadata
	{
		[Display(Name = nameof(DisplayRes.ScienceInterestName), ResourceType = typeof(DisplayRes))]
		public string Value { get; set; }
	}

	[MetadataType(typeof(ProjectMetadata))]
	public class Project : OrderedItem<string, int> { }

	public class ProjectMetadata
	{
		[Display(Name = nameof(DisplayRes.ProjectName), ResourceType = typeof(DisplayRes))]
		public string Value { get; set; }
	}

	[MetadataType(typeof(PublicationMetadata))]
	public class Publication : OrderedItem<string, DateTime> { }

	public class PublicationMetadata
	{
		[DataType(DataType.Date)]
		public DateTime Order { get; set; }

		[Display(Name = nameof(DisplayRes.PublicationName), ResourceType = typeof(DisplayRes))]
		public string Value { get; set; }
	}
}