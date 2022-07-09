using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandymanUIApp.Controllers
{
    public class Consumer : Controller
    {

        
        // GET: Consumer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Consumer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Consumer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Consumer/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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

        // GET: Consumer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Consumer/Delete/5
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
        
        //////private readonly HttpClient _client;
        //////public Consumer(HttpClient client)
        //////{
        //////    _client = client;
        //////}

        
        //private Task Authenticate()
        //{
        //    //... your authentication logic here
        //    _client.DefaultRequestHeaders.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    _client.DefaultRequestHeaders.Add("Auth",$"bearer {User?.Identity?.IsAuthenticated}" );
        //    return Task.CompletedTask;
        //}

    }
}
