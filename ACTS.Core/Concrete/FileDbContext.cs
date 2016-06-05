using Numeria.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Concrete
{
	public class FileDbContext: IDisposable
	{
		private static object _lockObj = new object();
		private static int _writeCounter;

		readonly public FileDB Files;
		readonly public FileAccess FileAccess;

		public FileDbContext() : this(FileAccess.ReadWrite) { }

		public FileDbContext(FileAccess access, string connectionString = "ACTSfilesConnection")
		{
			var fileDbConnectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
			var fileDbPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), fileDbConnectionString);
			Files = new FileDB(fileDbPath, access);

			if (access.HasFlag(FileAccess.Write))
				lock (_lockObj)
					_writeCounter++;
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					if (FileAccess.HasFlag(FileAccess.Write) && _writeCounter > 100)
					{
						lock (_lockObj)
							_writeCounter = 0;
						Files.Shrink();
					}
					Files.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~FileDbContext() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
