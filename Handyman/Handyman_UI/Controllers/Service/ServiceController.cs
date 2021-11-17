﻿using Handyman_UI.Models;
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
        private List<ServiceDisplayModel> services;

       

        public ServiceController(IServicesLoader servicesLoader)
        {
            _serviceLoader = servicesLoader;
           
        }


        // GET: Service
        public async Task<ActionResult> Index()
        {
            /*Display all Services*/
            if (services==null)
            {
               
                services = await _serviceLoader.getDisplayServices();
                return View(services);
            }
            else
            {
                return View(services);
            }
           
        }


        //A filter/search method for a service
        public async Task<ActionResult> Search(string serviceSearchString)
        {

            if (services == null)
            {

                services = await _serviceLoader.getDisplayServices();
                
            }
            var tempServices = new List<ServiceDisplayModel>();
                foreach (var service in services)
                {
                    if ((service.Name.Contains(serviceSearchString)) || (service.Category.Contains(serviceSearchString)))
                    {
                        tempServices.Add(service);

                    }
                }
           
            return View(tempServices);
           
        }

        // GET: Service/GetServices
        public async Task<ActionResult>  GetServices()
        {
            //this niggah actualy works
           
            return View();
        }
        // GET: Service/Details/5
        public async Task<ActionResult> Details(int id)
        {
            IEnumerable<ServiceDisplayModel> displayServices = await _serviceLoader.getDisplayServices();
            foreach (var service in displayServices) 
            {
                if (service.Id ==id)
                {
                    return View(service);
                }
            }
            return View();
        }

        public async Task<ActionResult> DisplayServiceCategory()
        {
            
            IEnumerable<ServiceDisplayModel> displayCategories = await _serviceLoader.getDisplayServices();
            return View(displayCategories);
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
            ////<ServiceModel> services = await _serviceLoader.LoadServices();
            return View(displayCategories);
           
        }
    }
}
