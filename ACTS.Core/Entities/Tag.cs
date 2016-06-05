using ACTS.Localization;
using System.Collections.Generic;

namespace ACTS.Core.Entities
{
	public class Tag
	{
		public int TagId { get; set; }

		[CustomRequired]
		public string Keyword { get; set; }

		public virtual IList<Post> Posts { get; set; } = new List<Post>();
	}
}