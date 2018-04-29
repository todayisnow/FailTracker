using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FailTracker.Web.Data;
using FailTracker.Web.Infrastructure;

namespace FailTracker.Web.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(ApplicationDbContext context)
		{
			//Do something with context!
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}