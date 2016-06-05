using ACTS.Core.Abstract;
using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Helpers
{
	public static class СontainsFileIdHelper
	{
		public static void UpdateFileForContainer(this IHaveFileId containsFileId, HttpPostedFileBase file)
		{
			if (file != null)
				using (var fileRepository = DependencyResolver.Current.GetService<IFileRepository>())
					containsFileId.FileId = containsFileId.FileId.HasValue ? fileRepository.UpdateFile(containsFileId.FileId.Value, file.FileName, file.InputStream)
																		   : fileRepository.CreateFile(file.FileName, file.InputStream);
			else
				containsFileId.FileId = null;
		}

		public static void CreateFileForContainer(this IHaveFileId containsFileId, HttpPostedFileBase file)
		{
			if (file != null)
				using (var fileRepository = DependencyResolver.Current.GetService<IFileRepository>())
					containsFileId.FileId = fileRepository.CreateFile(file.FileName, file.InputStream);
		}
	}
}
