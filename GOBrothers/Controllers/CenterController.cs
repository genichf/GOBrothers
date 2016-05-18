using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOBrothers.Controllers
{
    public class CenterController : Controller
    {
        // GET: Tile
        public ActionResult Develop()
        {
            string Name = "Центр розробки програмного забезпечення";
            string Content = @"Ми займаємось розробкою програмного забезпечення на таких технологіях, як C#, ASP.NET MVC, Python, Django.
                                Крім того самі створюємо дизайн сайта";
            ViewBag.Name = Name;
            ViewBag.Content = Content;

            return View();
        }

        // GET: Tile
        public ActionResult Design()
        {
            string Name = "Центр графіки та дизайну";
            string Content = @"Ми займаємось створенням дизайну, розробки логотипів, різної графічної продукції, а також web-дизайну.";
            ViewBag.Name = Name;
            ViewBag.Content = Content;

            return View();
        }

        // GET: Tile
        public ActionResult Professional()
        {
            string Name = "Ми пропонуємо професійні рішення";
            string Content = @"Ми займаємось професійним дизайном та розробкою ваших рішень.";
            ViewBag.Name = Name;
            ViewBag.Content = Content;

            return View();
        }
    }
}