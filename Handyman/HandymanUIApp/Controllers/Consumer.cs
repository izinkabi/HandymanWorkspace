using HandymanUILibrary.API;
using Microsoft.AspNetCore.Mvc;
using HandymanUIApp.Models;

namespace HandymanUIApp.Controllers
{
    public class Consumer : Controller
    {
      
        protected List<ServiceModel>? AllServices;
        private List<HandymanUILibrary.Models.ServiceModel> servicesData;
        private IServiceEndPoint? _serviceEndpoint;
        string errorString;
        private IAPIHelper _apiClient;

        public Consumer(IAPIHelper aPIHelper, IServiceEndPoint seviceEndPoint)
        {
            _apiClient = aPIHelper;
            _serviceEndpoint = seviceEndPoint;

        }
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
                s.Category = new CategoryModel();
                s.Category.Name = service.Name;
                s.Category.Description = service.Description;
                AllServices.Add(s);
            }
            return AllServices;
        }
        // GET: Consumer
        public async Task<ActionResult> Index()
        {
             
            try
            {
                var svs = await LoadServices();
                    return View(svs);
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return Redirect("Error");
            }
            
        }
       
        // GET: Consumer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Consumer/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Consumer/Create
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

        // GET: Consumer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Consumer/Edit/5
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
    }
}
