using System;
using System.Collections.Generic;
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

	}
}
