using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Helpers
{

    public class ExceptionsHelper: Controller
    {
        //This class allows handyman to hide the actual error messege from the end user
        //At same time it's delivering a meaningful error notification to the end user


        public ActionResult DisplayEmptyArrayError(string message)
        {
            return View();
        }

        public void DisplayNullReferenceError(string msg)
        {

        }

        public void UnauthorizedUserError()
        {

        }

        
    }
}