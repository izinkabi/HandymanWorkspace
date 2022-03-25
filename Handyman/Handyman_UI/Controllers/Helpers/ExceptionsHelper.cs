using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Helpers
{
    /// <summary>
    ///This class allows handyman to hide the actual error messege from the end user
    ///At same time it's delivering a meaningful error notification to the end user
    /// </summary>
    public class ExceptionsHelper: Controller
    {
        /// <summary>
        /// Display Empty Array
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult DisplayEmptyArrayError(string message)
        {
            return View();
        }
        /// <summary>
        /// Displaed Null Reference Error Hnadlig function
        /// </summary>
        /// <param name="msg"></param>
        public void DisplayNullReferenceError(string msg)
        {

        }
        /// <summary>
        /// Unauthorized User error handling
        /// </summary>
        public void UnauthorizedUserError()
        {

        }
        /// <summary>
        /// Page Not Found Error massage and redirect
        /// </summary>
        /// <returns></returns>
        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}