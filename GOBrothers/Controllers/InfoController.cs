using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOBrothers.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult About()
        {
            // ViewBag.Message = "Ми - компанія професіоналів.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ви можете зв’язатись з нами. Тут наша контактна інформація";

            return View();
        }
    }
}