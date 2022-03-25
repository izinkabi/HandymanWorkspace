using Handyman_UI.Controllers.Helpers;
using Handyman_UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Handyman_UI.Controllers
{
    public class ServiceController : Controller
    {
        private IServicesLoader _serviceLoader;
        private List<ServiceDisplayModel> services;
        private ExceptionsHelper _exceptionsHelper;
        private ExceptionsHelper DisplayExceptions; 
        
        string ErrorMsg;

       
<<<<<<< HEAD

        /// <summary>
        /// This is a constrator to render the Interfaces of a Instance of a classes
        /// </summary>
        /// <param name="servicesLoader"></param>
        /// <param name="exceptionHelper"></param>
=======
>>>>>>> 950a72eb6a3de88a5c2d1ec9950e40bd4785d55d
        public ServiceController(IServicesLoader servicesLoader,ExceptionsHelper exceptionHelper)
        {
            _serviceLoader = servicesLoader;
            DisplayExceptions = new ExceptionsHelper();
        }


        /// <summary>
        /// This methods gets the list of services to be displayed at start up of the webisite
        /// </summary>
        /// <returns>services</returns>
        public async Task<ActionResult> Index()
        {
            /*Display all Services*/
            if (services==null)
            {
                try
                {
                    services = await _serviceLoader.getDisplayServices();
                    //return View(services);
                }catch(Exception ex)
                {
                    ErrorMsg = ex.Message;
                    return View("Error");
                }
            }
            
           return View(services);
        }

        /// <summary>
        /// This is a method for seaarching for a service in a list of services 
        /// </summary>
        /// <param name="serviceSearchString"></param>
        /// <returns>tempServices - a list of service matching the passes in parameter</returns>
        public async Task<ActionResult> Search(string serviceSearchString)
        {
            var tempServices = new List<ServiceDisplayModel>();
            try
            {


                if (services == null)
                {

                    services = await _serviceLoader.getDisplayServices();
                }
               
                foreach (var service in services)
                {
                    if ((service.Name.Contains(serviceSearchString)) || (service.Category.Contains(serviceSearchString)))
                    {
                        tempServices.Add(service);

                    }
                }

               
            }catch(IndexOutOfRangeException ex)
            {
                DisplayExceptions.DisplayEmptyArrayError(ex.Message);
            }
            return View(tempServices);
        }

        /// <summary>
        /// This function gets a list of services to be Diplayed
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult>  GetServices()
        {           
            return View();
        }
        /// <summary>
        /// This method takes in an ID to find details about a service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method is to Display a list of different types of service categories
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> DisplayServiceCategory()
        {
            
            IEnumerable<ServiceDisplayModel> displayCategories = await _serviceLoader.getDisplayServices();
            return View(displayCategories);
        }



        //TODO - Remove this Test function if it is no longer in use
        public async Task<ActionResult> Test()
        {
            IEnumerable<ServiceDisplayModel> displayCategories = await _serviceLoader.getDisplayServices();
            ////<ServiceModel> services = await _serviceLoader.LoadServices();
            return View(displayCategories);
           
        }
    }
}
