﻿using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class FileRepository : IFileRepository
	{
		private FileDbContext _context;

		private FileDbContext GerContext(FileAccess access)
		{
			if (_context == null)
				return _context = new FileDbContext(access);

			if (_context.FileAccess.HasFlag(access))
				return _context;

			_context.Dispose();
			return _context = new FileDbContext(access);
		}

		public Guid CreateFile(string fileName, Stream input) => GerContext(FileAccess.Write).Files.Store(fileName, input).ID;

		public Guid CreateFile(string fileName) => GerContext(FileAccess.Write).Files.Store(fileName).ID;

		public bool DeleteFile(string fileId) => DeleteFile(Guid.Parse(fileId));

		public bool DeleteFile(Guid fileId) => GerContext(FileAccess.Write).Files.Delete(fileId);

		public StoredFile GetFile(string fileId) => GetFile(Guid.Parse(fileId));

		public StoredFile GetFile(Guid fileId)
		{
			var storedFile = new StoredFile();
			var fileInfo = GerContext(FileAccess.Read).Files.Read(fileId, storedFile);
			storedFile.Name = fileInfo.FileName;
			storedFile.MimeType = fileInfo.MimeType;
			storedFile.FileLength = fileInfo.FileLength;

			return storedFile;
		}

		public Guid UpdateFile(string fileId, string fileName, Stream input) => 
			UpdateFile(Guid.Parse(fileId), fileName, input);

		public Guid UpdateFile(Guid fileId, string fileName, Stream input)
		{
			var context = GerContext(FileAccess.ReadWrite);
			context.Files.Delete(fileId);
			return context.Files.Store(fileName, input).ID;
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
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.
				_context = null;

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~FileRepository() {
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
