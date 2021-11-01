using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.ServiceProvider
{
    public class spController : Controller
    {
        // GET: spHome
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Requests()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult ActiveRequests()
        {
            return View();
        }
    }
}