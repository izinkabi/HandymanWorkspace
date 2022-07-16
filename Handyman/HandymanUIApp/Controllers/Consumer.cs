using HandymanUILibrary.API;
using Microsoft.AspNetCore.Mvc;
using HandymanUIApp.Models;

namespace HandymanUIApp.Controllers
{
    public class Consumer : Controller
    {
      
        
        string errorString;
        private IAPIHelper _apiClient;

        public Consumer(IAPIHelper aPIHelper)
        {
            _apiClient = aPIHelper;
            //_serviceEndpoint = seviceEndPoint;

        }
        
        // GET: Consumer
        
       
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

        // POST: Consumer/Create
        //Called when a customer calls a consumer
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
