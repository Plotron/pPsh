using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pPsh.Models;

namespace pPsh.Controllers
{
    public class HomeController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            NameValueCollection statistics = new NameValueCollection()
            {
                {"Devices",  db.Devices.Count().ToString()},
                {"Scenarios", db.Scenarios.Count().ToString() },
                {"Users", db.Profiles.Count().ToString() }
            };

            ViewBag.Statistics = statistics;

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Api()
        {
            return View();
        }
    }
}