using ACTS.Core.Entities;
using ACTS.Core.Logging;
using System.Linq;

namespace ACTS.Core.Abstract
{
	public interface ILogRepository
	{
		IQueryable<LogEntry> LogEntries { get; }
		IQueryable<LogEntry> LastMonthLogs { get; }
		IQueryable<LogEntry> LastWeekLogs { get; }
		IQueryable<LogEntry> LastLogsForNomDays(double nomDays);
	}
}