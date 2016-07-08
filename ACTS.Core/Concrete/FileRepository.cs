using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using ACTS.Core.Entities;
using System.Linq.Expressions;
using System.Data.Entity;

namespace ACTS.Core.Concrete
{
	public class FileRepository : IFileRepository
	{
		private FileDbContext _context;

		private FileDbContext GetContext(FileAccess access)
		{
			if (_context == null)
				return _context = new FileDbContext(access);

			if (_context.FileAccess.HasFlag(access))
				return _context;

			_context.Dispose();
			return _context = new FileDbContext(access);
		}

		public Guid CreateFile(string fileName, Stream input) => GetContext(FileAccess.Write).Files.Store(fileName, input).ID;

		public Guid CreateFile(string fileName) => GetContext(FileAccess.Write).Files.Store(fileName).ID;

		public bool DeleteFile(string id) => DeleteFile(Guid.Parse(id));

		public bool DeleteFile(Guid id) => GetContext(FileAccess.Write).Files.Delete(id);

		public StoredFileStream GetFile(string id) => GetFile(Guid.Parse(id));

		public StoredFileStream GetFile(Guid id)
		{
			var storedFile = new StoredFileStream();
			var fileInfo = GetContext(FileAccess.Read).Files.Read(id, storedFile);
			storedFile.Name = fileInfo.FileName;
			storedFile.MimeType = fileInfo.MimeType;
			storedFile.FileLength = fileInfo.FileLength;

			return storedFile;
		}

		public Guid UpdateFile(string id, string fileName, Stream input) =>
			UpdateFile(Guid.Parse(id), fileName, input);

		public Guid UpdateFile(Guid id, string fileName, Stream input)
		{
			var context = GetContext(FileAccess.ReadWrite);
			context.Files.Delete(id);
			return context.Files.Store(fileName, input).ID;
		}

		public IQueryable<StoredFileInfo> FileInfos
		{
			get
			{
				FileDbContext fileContext = GetContext(FileAccess.Read);
                var storedFileInfos = fileContext.Files.ListFiles()
                                                       .Select(f => new StoredFileInfo
                                                       {
                                                           Id = f.ID,
                                                           Name = f.FileName,
                                                           Size = f.FileLength
                                                       });

				using (EFDbContext efContext = new EFDbContext())
				{
					var entitysWithFileId = efContext.Uncos.Where(n => n.ImageId.HasValue)
														   .AsNoTracking()
														   .AsEnumerable<IHaveFileId>()
											.Union(efContext.Events.Where(ev => ev.ImageId.HasValue)
																   .AsNoTracking()
																   .AsEnumerable<IHaveFileId>())
											.Union(efContext.Employees.Where(empl => empl.PhotoId.HasValue)
																	  .AsNoTracking()
																	  .AsEnumerable<IHaveFileId>())
											.Union(efContext.Teachers.Where(t => t.PhotoId.HasValue)
																	 .AsNoTracking()
																	 .AsEnumerable<IHaveFileId>())
											.GroupBy(hfi => hfi.FileId.Value);

                    storedFileInfos = entitysWithFileId.Join(
                        storedFileInfos, 
                        ewfi => ewfi.Key,
                        sfi => sfi.Id, 
                        (ewfi, sfi) => { sfi.Users = ewfi; return sfi; });
                }

				return storedFileInfos.AsQueryable();
			}
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
					_context?.Dispose();
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