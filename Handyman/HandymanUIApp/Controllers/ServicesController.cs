using HandymanUIApp.Models;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandymanUIApp.Controllers
{
    public class ServicesController : Controller
    {
        internal List<ServiceModel> AllServices;
        private IServiceEndPoint _serviceEndPoint;
        private List<HandymanUILibrary.Models.ServiceModel> servicesData;
        //Constructing the IService service
        string errorString;
        public ServicesController(IServiceEndPoint serviceEndPoint) 
        {
            _serviceEndPoint = serviceEndPoint;
        }

        //Load the services from the database
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
                s.Description = service.Description;
                s.ImageUrl = service.ImageUrl;
                s.Id = service.Id;
                s.CategoryName = service.Name;
                s.CategoryDescription = service.Description;
                s.CategoryType = service.Type;
                AllServices.Add(s);
            }
            return AllServices;
        }
           
        //Display Services 
        public async Task<ActionResult> ServiceHome()
        {
            if (AllServices == null)
            {
                AllServices = await LoadServices();
                return View(AllServices);
            }
            return View();
        }


        public async Task<ActionResult> TranfereServiceToOrder(int id)
        { 
            if (AllServices == null)
            {
                AllServices = await LoadServices();
            }

            foreach (var service in AllServices)
            {
                if (service.Id == id)
                {
                    //TempData["NewOrder"] = service;
                    return RedirectToAction(actionName: "CreateOrder", controllerName :"Orders", service);
                }
            }
            return View();
        }

        

        // GET: Services/Details/5
        //Load a single service
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
    }
}
