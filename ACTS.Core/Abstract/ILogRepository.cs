using System;
using System.Linq;
using ACTS.Core.Logging;

namespace ACTS.Core.Abstract
{
	public interface ILogRepository
	{
		IQueryable<LogEntry> LastMonthLogs { get; }
		IQueryable<LogEntry> LastWeekLogs { get; }
		IQueryable<LogEntry> LogEntries { get; }

		IQueryable<LogEntry> GetByDateAndLevel(DateTime start, DateTime end, LogLevel level);
		IQueryable<LogEntry> LastLogsByNomDays(double nomDays);
		IQueryable<LogEntry> LastLogsByNomDaysAndLevel(double nomDays, LogLevel level);
		IQueryable<LogEntry> LastLogsByDateAndLevel(DateTime fromData, LogLevel level);
	}
}