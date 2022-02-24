using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Controllers.Helpers
{
    public class ExceptionsHelper:IExceptionsHelper
    {
        //This class allows handyman to hide the actual error messege from the end user
        //At same time it's delivering a meaningful error notification to the end user


        public void DisplayEmptyArrayError(string message)
        {

        }

        public void DisplayNullReferenceError(string msg)
        {

        }

        public void UnauthorizedUserError()
        {

        }

       
    }
}