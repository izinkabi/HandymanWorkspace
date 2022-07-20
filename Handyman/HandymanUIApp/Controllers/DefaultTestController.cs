using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandymanUIApp.Controllers
{
    public class DefaultTestController : Controller
    {
        // GET: DefaultTestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DefaultTestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DefaultTestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DefaultTestController/Create
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

        // GET: DefaultTestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DefaultTestController/Edit/5
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

        // GET: DefaultTestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DefaultTestController/Delete/5
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
