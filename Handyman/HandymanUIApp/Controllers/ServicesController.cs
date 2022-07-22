using HandymanUIApp.Models;
using HandymanUILibrary.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandymanUIApp.Controllers
{
    public class ServicesController : Controller
    {
        internal List<ServiceModel>? AllServices;
        internal List<ServiceCategoryModel>? AllServiceCategories;  
        private IServiceEndPoint _serviceEndPoint;
        private List<HandymanUILibrary.Models.ServiceModel>? servicesData;
        private List<HandymanUILibrary.Models.ServiceCategoryModel> serviceCategoriesData;


        //Constructing the IService service
        string? errorString;
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
                servicesData = await _serviceEndPoint.GetServices();
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
                s.CategoryId = service.CategoryId;
                s.CategoryName = service.CategoryName;
                s.CategoryDescription = service.CategoryDescription;
                s.CategoryType = service.Type;              
                AllServices.Add(s);
            }
            return AllServices;
        }

        //Load service categories and their services
        private async Task<List<ServiceCategoryModel>> LoadServiceCategories()
        {
            AllServiceCategories = new List<ServiceCategoryModel>();
            serviceCategoriesData = new();
           

            try
            {
                
                serviceCategoriesData = await _serviceEndPoint.GetServiceCategories();//await the categories
                //Populate the category
                foreach(var serviceCategory in serviceCategoriesData)
                {
                    var Category = new ServiceCategoryModel();
                    Category.CategoryDescription = serviceCategory.CategoryDescription;
                    Category.CategoryId = serviceCategory.CategoryId;
                    Category.CategoryName = serviceCategory.CategoryName;
                    
                    //IEnumerable<ServiceModel> searchResult;
                   
                    if (Category.Services != null )
                    {
                        AllServices = await LoadServices();
                            //Category.Services = new List<ServiceModel>();
                            //searchResult = AllServices.Where(s => s.Id.ToString()!.Equals(serviceCategory.CategoryId.ToString()));
                            //Category.Services?.AddRange(searchResult);
                        foreach(var service in AllServices)
                        {
                            if (service.CategoryId == serviceCategory.CategoryId)
                            {
                                Category.Services?.Add(service);
                            }
                        }


                        AllServiceCategories.Add(Category);
                    }

                    errorString = null;
                }
            }
            catch(Exception ex)
            {
                errorString = ex.Message;
                AllServiceCategories = null;
            }
            return AllServiceCategories;
        }

        //Displaying the service categories
        [HttpGet,Route("ServiceCategoriesDisplay")]
        public async Task<ActionResult> ServiceCategoriesDisplay()
        {
            var cats = await LoadServiceCategories();
            return View(cats);
        }


        //Display Services Default
        public async Task<ActionResult> ServiceHome()
        {
            if (AllServices == null)
            {
                AllServices = await LoadServices();
                return View(AllServices);
            }
            return View();
        }

        //Helper method for transferring a service into an order
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

        //Search for service(s) by service Name / Id 
        [HttpGet, Route("SearchServiceByName")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<ActionResult> SearchServiceByName(string searchString)
        {
            if(AllServices == null)
            {
                AllServices = await LoadServices();
            }

            IEnumerable<ServiceModel> searchResult;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                searchResult = AllServices.Where(s => s.Name.ToLower()!.Contains(searchString.ToLower()));//make all search strings lowercase to avoid casing
                return View(nameof(SearchResults),searchResult.ToList());
            }

            return View("Error");
        }
        //Placeholder for search results partailview
        public IActionResult SearchResults()
        {
            return View();
        }


        //Search service by category Name/Id
        [HttpGet, Route("SearchServicesByCategoryName")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> SearchServicesByCategoryName(string searchString)
        {
            //make sure there are services from the database
            if (AllServices == null)
            {
                AllServices = await LoadServices();
            }

            IEnumerable<ServiceModel> searchResult;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchResult = AllServices.Where(s => s.Name.ToLower()!.Contains(searchString.ToLower()));
                return View(nameof(SearchResults), searchResult.ToList());
            }

            return View("Error");
        }

        // GET: Services/Details/5
        //Load a single service
        [ResponseCache(Duration = 60 , Location = ResponseCacheLocation.Client, NoStore = false)]
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
