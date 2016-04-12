using ACTS.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ACTS.UI.Services
{
	public static class LogService
	{
		public static string DefaultPathToLogs { get; set; }

		private const string _logFileFormat = "ex{0:yyMM}.log";

		private static ILogger _logger;
		static LogService()
		{
			IKernel kernel = new StandardKernel();
			_logger = kernel.Get<ILoggerFactory>().GetCurrentClassLogger();
		}

		public static async Task<IQueryable<LogEntry>> GetLogsAsync(DateTime start, DateTime end)
		{
			if (start > end)
				return Enumerable.Empty<LogEntry>().AsQueryable();

			DateTime localStart = start.ToLocalTime(), localEnd = end.ToLocalTime();

			StringBuilder logsSB = new StringBuilder();

			for (var date = localStart - new TimeSpan(localStart.Day - 1, 0, 0, 0); date <= localEnd; date = date.AddMonths(1))
			{
				var logFilePath = Path.Combine(DefaultPathToLogs, string.Format(_logFileFormat, date));

				if (File.Exists(logFilePath))
					try
					{
						logsSB.Append(File.ReadAllText(logFilePath));
					}
					catch (IOException ex)
					{
						_logger.ErrorException(ex.Message, ex);
					}
			}

			logsSB.Insert(0, '[');
			logsSB.Replace('\n', ',', 0, logsSB.Length - 1);
			logsSB.Append(']');
			var logs = (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<LogEntry>>(logsSB.ToString()))).AsQueryable();
			return logs.Where(log => log.UtcDate >= start && log.UtcDate <= end);
		}

		public static IQueryable<LogEntry> GetLogs(DateTime start, DateTime end)
		{
			if (start > end)
				return Enumerable.Empty<LogEntry>().AsQueryable();

			StringBuilder logSB = new StringBuilder();

			for (var date = start; date <= end; date = date.AddMonths(1))
			{
				var logFileInfo = new FileInfo(Path.Combine(DefaultPathToLogs, string.Format(_logFileFormat, date)));
				if (logFileInfo.Exists)
					using (var logFileReader = logFileInfo.OpenText())
						logSB.Append(logFileReader.ReadToEnd());
			}

			var logs = JsonConvert.DeserializeObject<IEnumerable<LogEntry>>(logSB.ToString()).AsQueryable();

			return logs.Where(log => log.UtcDate >= start && log.UtcDate <= end);
		}

		public static async Task<IQueryable<LogEntry>> GetLastMonthLogsAsync()
		{
			var now = DateTime.UtcNow;
			return await GetLogsAsync(now.AddMonths(-1), now);
		}

		public static async Task<IQueryable<LogEntry>> GetLastWeekLogsAsync()
		{
			var now = DateTime.UtcNow;
			return await GetLogsAsync(now.AddDays(-7d), now);
		}

		public static async Task<IQueryable<LogEntry>> GetByDateAndLevelAsync(DateTime start, DateTime end, LogLevel level)
		{
			return (await GetLogsAsync(start, end)).Where(log => log.Level >= level);
		}
	}
}
