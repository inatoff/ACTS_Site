using ACTS.Core.Entities;
using Numeria.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface IFileRepository : IDisposable
	{
		Guid UpdateFile(Guid id, string fileName, Stream input);
		Guid CreateFile(string fileName, Stream input);
		StoredFileStream GetFile(Guid id);
		bool DeleteFile(Guid id);
		IQueryable<StoredFileInfo> FileInfos { get; }
	}
}