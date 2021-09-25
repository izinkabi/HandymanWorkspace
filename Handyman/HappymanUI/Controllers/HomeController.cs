using HandymanUILibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HappymanUI.Controllers
{
    public class HomeController : Controller
    {
        private IAPIHelper _apiHelper;
        private string _useName;
        private string _password;
        public string ErrorMessage { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string UserName
        {
            get { return _useName; }
            set
            {
                _useName = value;
               // NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
            }

        }


        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                //NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        private void NotifyOfPropertyChange(Func<bool> p)
        {
            throw new NotImplementedException();
        }

        public bool CanLogin
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }
        public async Task btn_login()
        {
            try
            {
                ErrorMessage = "";
                var results = await _apiHelper.AuthenticateUser(UserName, Password);
                //User infor from a Loggedinuser 
                await _apiHelper.GetLoggedInUserInfor(results.Access_Token);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }

        }
    }
}