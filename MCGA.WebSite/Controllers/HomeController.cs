using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCGA.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
			ViewBag.Message = "Clínica Alejandría";

			return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Clínica Alejandría";

            return View();
        }
    }
}