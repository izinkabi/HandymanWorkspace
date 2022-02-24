using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Requests
{
    public class RequestsController : Controller
    {
        // GET: Requests
        public ActionResult Index()
        {
            return View();
        }

        // GET: Requests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Requests/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Requests/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
