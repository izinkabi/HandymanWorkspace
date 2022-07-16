using HandymanUIApp.Models;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandymanUIApp.Controllers
{
    public class Services : Controller
    {
        private List<ServiceModel> AllServices;
        private IServiceEndPoint _serviceEndPoint;
        private List<HandymanUILibrary.Models.ServiceModel> servicesData;
        string errorString;
        public Services(IServiceEndPoint serviceEndPoint) 
        {
            _serviceEndPoint = serviceEndPoint;
        }
        private async Task<List<ServiceModel>> LoadServices()
        {
            AllServices = new List<ServiceModel>();
            servicesData = new List<HandymanUILibrary.Models.ServiceModel>();
            try
            {
                servicesData = await _serviceEndPoint?.GetServices();
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
            }

            foreach (var service in servicesData)
            {
                //populating a service to UI model
                var s = new ServiceModel();
                s.Name = service.Name;
                s.Decription = service.Description;
                s.ImageUrl = service.ImageUrl;
                s.Id = service.Id;
                s.Category = new CategoryModel();
                s.Category.Name = service.Name;
                s.Category.Description = service.Description;
                AllServices.Add(s);
            }
            return AllServices;
        }
            // GET: Services
            public async Task<ActionResult> ServiceHome()
            {
                if (AllServices == null)
                {
                    AllServices = await LoadServices();
                    return View(AllServices);
                }
                return View();
            }

        // GET: Services/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (AllServices == null)
            {
                AllServices = await LoadServices();
            }
               
            foreach (var service in AllServices)
            {
                if(service.Id == id)
                {
                    return View(service);
                }    
            }
            return View();
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Services/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
