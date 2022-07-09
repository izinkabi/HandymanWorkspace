using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Handyman_UI_S.Models;
using Syncfusion.EJ2.Schedule;

namespace Handyman_UI_S.Controllers
{
    /// <summary>
    /// This home controller uses SSL insttead of HTTPS
    /// </summary>
    [RequireHttps]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            spacingModel modelValue = new spacingModel();
            modelValue.cellSpacing = new double[] { 10, 10 };
            return View(modelValue);
        }

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LogOff()
        {


            return View();
        }
    }
}