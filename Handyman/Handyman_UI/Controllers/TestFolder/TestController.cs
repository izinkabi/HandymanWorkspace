using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.TestFolder
{
	public class TestController : Controller
	{
		// GET: Test
		public ActionResult Index()
		{
			return View();
		}

		// GET: Test/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Test/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Test/Create
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

		// GET: Test/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Test/Edit/5
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

		// GET: Test/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Test/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				Console.ReadLine();
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}


		//private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		//protected void Application_Error()
		//{
		//	var ex = Server.GetLastError();
		//	logger.Info(ex.Message.ToString() + Environment.NewLine + DateTime.Now);
		//	HttpContext.Current.ClearError();
		//	Response.Redirect(“~/ Error / Index”, false);
		//}
	//}
	}





}

