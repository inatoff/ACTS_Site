using ACTS.Core.Abstract;
using ACTS.UI.Controllers;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Controllers
{
	public class FileViewerController : BaseController
	{

		private IFileRepository _fileRepository;

		public FileViewerController(IFileRepository fileRep)
		{
			_fileRepository = fileRep;
		}

		public ActionResult Index()
		{
			var filesInfo = _fileRepository.FileInfos;
			return View(filesInfo);
		}
	}
}