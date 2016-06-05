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
	public interface IFileRepository:IDisposable
	{
		Guid UpdateFile(Guid fileId, string fileName, Stream input);
		Guid CreateFile(string fileName, Stream input);
		StoredFile GetFile(Guid fileId);
		bool DeleteFile(Guid fileId);
	}
}
