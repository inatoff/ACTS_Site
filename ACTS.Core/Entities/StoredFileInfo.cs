using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Abstract;

namespace ACTS.Core.Entities
{
	public class StoredFileInfo
	{
		public Guid Id { get; internal protected set; }
		public string Name { get; internal protected set; }
		public uint Size { get; internal protected set; }
		public IEnumerable<IHaveFileId> Users { get; internal set; }
	}
}
