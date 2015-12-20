using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTS.UI.Controllers
{
	public class AdminController : BaseController
	{
		//private INewsRepository _newsRepository;

		//public AdminController(INewsRepository newsRepository)
		//{
		//	_newsRepository = newsRepository;
		//}

		public ActionResult Dashboard()
		{
			return View();
		}
	}
}