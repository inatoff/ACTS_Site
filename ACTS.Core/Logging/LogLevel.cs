using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Logging
{
	public enum LogLevel : byte
	{
		Trace,
		Debug,
		Info,
		Warn,
		Error,
		Fatal,
		Off
	}
}
