using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Helpers
{
<<<<<<< HEAD
=======

>>>>>>> 2a2a685f4645639256f75ad2965922ee1458c077
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

<<<<<<< HEAD

        //404 code exception
        public ActionResult PageNotFound()
        {
            return View();
        }



=======
        
>>>>>>> 2a2a685f4645639256f75ad2965922ee1458c077
    }
}