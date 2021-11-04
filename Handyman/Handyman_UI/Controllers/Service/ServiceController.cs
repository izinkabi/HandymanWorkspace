using Handyman_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers
{
    public class ServiceController : Controller
    {
        private IServicesLoader _serviceLoader;
        public ServiceController(IServicesLoader servicesLoader)
        {
            _serviceLoader = servicesLoader;
        }


        // GET: Service
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Service/GetServices
        public async Task<ActionResult>  GetServices()
        {
            //this niggah actualy works
           
            return View();
        }
        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult DisplayServiceCategory()
        {
            return View();
        }
        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
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

        // GET: Service/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Service/Edit/5
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

        // GET: Service/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Service/Delete/5
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
        public async Task<ActionResult> Test()
        {
            IEnumerable<ServiceDisplayModel> displayCategories = await _serviceLoader.getDisplayServices();
            //<ServiceModel> services = await _serviceLoader.LoadServices();
            return View(displayCategories);
        }
    }
}
