using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Logging
{
	public class LogEntry
	{
		public int LogEntryId { get; set; }
		[Required]
		public string CallSite { get; set; }
		public DateTime UtcDate { get; set; }
		public string Exception { get; set; }
		public LogLevel Level { get; set; }
		[Required]
		[MaxLength(80)]
		public string Logger { get; set; }
		[Required]
		public string MachineName { get; set; }
		[MaxLength(256)]
		public string Message { get; set; }
		[Required]
		public string StackTrace { get; set; }
		[Required]
		[MaxLength(10)]
		public string Thread { get; set; }
		[Required]
		[MaxLength(256)]
		public string Username { get; set; }
	}
}
