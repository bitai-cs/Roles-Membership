using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzManAspNetIdentity.TestMVC.Controllers {
	[Authorize]
	public class HomeController : Controller {
		[AllowAnonymous]
		public ActionResult Index() {
			return View();
		}

		[Authorize(Roles = "Administrador")]
		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		[AllowAnonymous]
		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}