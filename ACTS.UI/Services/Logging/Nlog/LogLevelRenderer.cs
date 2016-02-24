using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Services.Logging.Nlog
{
	[LayoutRenderer("enumLevel")]
	[ThreadAgnostic]
	public class LogLevelRenderer : LayoutRenderer
	{
		///
		/// Renders the current date and appends it to the specified .
		///
		/// <param name="builder">The  to append the rendered data to.
		/// <param name="logEvent">Logging event.
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.Level.Ordinal);
		}
	}
}
