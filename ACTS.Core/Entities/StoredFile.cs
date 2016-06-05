using ACTS.Localization.Resources;
using Numeria.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class StoredFile : MemoryStream
	{
		public string Name { get; internal set; }
		public string MimeType { get; internal set; }
		public uint FileLength { get; internal set; }
	}
}
