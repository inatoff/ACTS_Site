using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class FileController : BaseController
	{
		private IFileRepository _repository;

		public FileController(IFileRepository repository)
		{
            _repository = repository;
		}

		[Route("Files/Id{id}", Name = "ToDefaultActionFileController")]
		public ActionResult Index(Guid id)
		{
			var file = _repository.GetFile(id);

			Response.Buffer = false;
			Response.BufferOutput = false;
			Response.ContentType = file.MimeType;
			Response.AppendHeader("Content-Length", file.FileLength.ToString());
			Response.AppendHeader("content-disposition", "inline; filename=" + file.Name);

			file.WriteTo(Response.OutputStream);

			return new EmptyResult();
		}
	}
}