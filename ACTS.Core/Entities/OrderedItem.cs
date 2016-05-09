using ACTS.Localization;
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

	public class Discipline : OrderedItem<string, int>
	{
	}
	public class ScienceInterest : OrderedItem<string, int>
	{
	}
	public class Project : OrderedItem<string, int>
	{
	}
	public class Publication : OrderedItem<string, DateTime>
	{
	}
}