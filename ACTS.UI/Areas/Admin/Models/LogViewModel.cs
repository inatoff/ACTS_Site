using ACTS.Core.Logging;
using ACTS.Localization.Resources;
using ACTS.UI.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class LogViewModel
	{
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
		[Display(Name = nameof(DisplayRes.StartDate), ResourceType = typeof(DisplayRes))]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
		public DateTime EndDate { get; set; }

		public IEnumerable<LogEntry> Logs { get; set; }

		[Display(Name = nameof(DisplayRes.Level),ResourceType = typeof(DisplayRes))]
		public LogLevel LogLevel { get; set; }

		public LogViewModel(LogLevel level, DateTime start, DateTime end, IEnumerable<LogEntry> logs)
		{
			StartDate = start;
			EndDate = end;
			Logs = logs;
			LogLevel = level;
		}

		public LogViewModel(LogLevel level, DateTime start, IEnumerable<LogEntry> logs)
		{
			StartDate = start;
			EndDate = DateTime.Now;
			Logs = logs;
			LogLevel = level;
		}
	}
}
