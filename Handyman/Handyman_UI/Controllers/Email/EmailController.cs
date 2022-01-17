using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Email
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult ComposeEmail()
        {
            return View();
        }
        public ActionResult Read()
        {
            return View();
        }
    }
}