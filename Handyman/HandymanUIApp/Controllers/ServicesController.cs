using Microsoft.AspNetCore.Mvc;
using HandymanUIApp.Models;
using HandymanUILibrary.API;

namespace HandymanUIApp.Controllers
{
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly ILogger<ServicesController> _logger;
        private string  errorString;
        protected List<ServiceModel>? AllServices;
        private List<HandymanUILibrary.Models.ServiceModel> servicesData;
        private IServiceEndPoint? _serviceEndpoint;
       

        //Construct the end service point
        public ServicesController(ILogger<ServicesController> logger, IServiceEndPoint serviceEnd)
        {
            _logger = logger;
            _serviceEndpoint = serviceEnd;
        }
    
        //Home views for the services
        public async Task<ActionResult> DisplyServices()
        {
            if (AllServices==null)
            {
                var svs = await LoadServices();
                return View(svs);

            }
            return View();
        }

        //Load and assign the services
        internal async Task<IEnumerable<ServiceModel>> LoadServices()
        {
            AllServices = new List<ServiceModel>();
            servicesData =  new List<HandymanUILibrary.Models.ServiceModel>();
            try
            {
                servicesData = await _serviceEndpoint?.GetServices();
            }catch(Exception ex)
            {
                errorString = ex.Message;
            }
            
            foreach(var service in servicesData)
            {
                //populating a service to UI model
                var s = new ServiceModel();
                s.Name = service.Name;
                s.Decription = service.Description;
                s.ImageUrl = service.ImageUrl;
                s.Id = service.Id;
              
                s.CategoryName = service.Name;
                s.CategoryDescription = service.Description;
                
                AllServices.Add(s);
            }
            return AllServices;
        }


        //Error Action Method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}