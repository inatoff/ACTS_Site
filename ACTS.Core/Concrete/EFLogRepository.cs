using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;
using ACTS.Core.Logging;
using System.Data.Entity;

namespace ACTS.Core.Concrete
{
	public class EFLogRepository : ILogRepository
	{
		private EFDbContext context = new EFDbContext();

		public IQueryable<LogEntry> LastMonthLogs
		{
			get
			{
				return context.LogEntries.Where(log => DbFunctions.DiffDays(log.UtcDate, DateTime.UtcNow) < 30);
			}
		}

		public IQueryable<LogEntry> LastWeekLogs
		{
			get
			{
				return context.LogEntries.Where(log => DbFunctions.DiffDays(log.UtcDate, DateTime.UtcNow) < 7);
			}
		}

		public IQueryable<LogEntry> LogEntries
		{
			get
			{
				return context.LogEntries;
			}
		}

		public IQueryable<LogEntry> LastLogsForNomDays(double nomDays)
		{
			return context.LogEntries.Where(log => (DateTime.UtcNow - log.UtcDate).TotalDays < nomDays);
		}
	} 
}
