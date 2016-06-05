using ACTS.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using Ninject.Extensions.Logging;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Services.Logging
{
	public static class LogService
	{
		private const string _logFileFormat = "ex{0:yyMM}.log";

		private static ILogger _logger;
		private static string _pathToLogs;

		private static ILogger Logger
		{
			get
			{
				return _logger ?? (_logger = DependencyResolver.Current.GetService<ILoggerFactory>().GetCurrentClassLogger());
			}
		}

		public static string PathToLogs {
			get
			{
				if (_pathToLogs == null)
				{
					var jsonLayout = new LoggingConfiguration().AllTargets.OfType<FileTarget>().FirstOrDefault(ft => ft.Layout is JsonLayout);
					_pathToLogs = Path.GetDirectoryName(jsonLayout?.FileName.Render(NLog.LogEventInfo.CreateNullEvent()));
				}
				return _pathToLogs;
			}
			set
			{
				_pathToLogs = value;
			}
		}

		public static async Task<IQueryable<LogEntry>> GetLogsAsync(DateTime start, DateTime end)
		{
			if (start > end)
				return Enumerable.Empty<LogEntry>().AsQueryable();

			DateTime startLocal = start.ToLocalTime(), endLocal = end.ToLocalTime();

			StringBuilder logsSB = new StringBuilder();

			for (var date = startLocal.Date.AddDays(1 - startLocal.Day); date <= endLocal; date = date.AddMonths(1))
			{
				var logFilePath = Path.Combine(PathToLogs, string.Format(_logFileFormat, date));

				if (File.Exists(logFilePath))
					// написавший это знает что довольно опасно использует цикл и что возможно зацикливание,
					// но так как он еще тот говнокодер, то просто не знает как получить доступ до файла если
					// его использует другой процесс
					// ЗЫ: есть идеи как сделать лучше напишите сами или отпишите мне
					do
					{
						try
						{
							var task = Task.Factory.StartNew(() => File.ReadAllText(logFilePath));
#if DEBUG
#pragma warning disable CS4014 
							task.ContinueWith(s => Logger.Trace($"Read text from {logFilePath} successful."));
#pragma warning restore CS4014
#endif
							logsSB.Append(await task);
							break;
						}
						catch (IOException ex)
						{
							Logger.ErrorException(ex.Message, ex);
						}
					} while (true);
			}

			logsSB.Insert(0, '[');
			logsSB.Replace('\n', ',', 0, logsSB.Length - 1);
			logsSB.Append(']');
			var logs = (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<LogEntry>>(logsSB.ToString()))).AsQueryable();
			DateTime startUtc = start.ToUniversalTime(), endUtc = end.ToUniversalTime();

			return logs.Where(log => log.UtcDate >= startUtc && log.UtcDate <= endUtc);
		}

		public static IQueryable<LogEntry> GetLogs(DateTime start, DateTime end)
		{
			if (start > end)
				return Enumerable.Empty<LogEntry>().AsQueryable();

			StringBuilder logSB = new StringBuilder();

			for (var date = start; date <= end; date = date.AddMonths(1))
			{
				var logFileInfo = new FileInfo(Path.Combine(PathToLogs, string.Format(_logFileFormat, date)));
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
