using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public class CustomerHomeController : Controller
    {

        private IAPIHelper _apiHepler;
        private IloggedInUserModel _loggedInUserModel;

        private HandymanUILibrary.Models.NewUserModel user;
        private HandymanUILibrary.Models.ProfileModel profileModel;

        static private string DisplayUserName;
        static private string Username;
        static private string Password;

        private IProfileEndPoint _profileEndPoint;
        private IConsumerEndPoint _consumerEndPoint;
        //private IRegisterProviderEndPoint _registerProviderEndpoint;
       
        private bool loggedIn;
        

        //The use of constructor allows for the Imterfaces to be rendered
        public CustomerHomeController(IAPIHelper aPIHelper,IProfileEndPoint profileEndpoint,IConsumerEndPoint consumerEndpoint)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profileEndpoint;
            _consumerEndPoint = consumerEndpoint;
        }

        

        //Home page Action funtion
        public  async Task<ActionResult> Index()
        {
          
            return View();
        }

        public async Task<ActionResult> ElectronicCategory()
        {
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            
            return View();
        }
        public ActionResult Services()
        {

            ViewBag.Message = "Your application description page.";
           
            return View();
        }

   
        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult Contact()
        {
            
            ViewBag.Message = "Your contact page.";
            
            return View();
        }

        //Here we are trying to register the consumer with having to create a view for it,
        //and it is referenced inside a string operand from CreateProfile()'s return statement
        public async Task<RedirectToRouteResult> RegisterCustomer()
        {
            try
            {
                var token = Session["Token"].ToString();
                var loggedUser = await _apiHepler.GetLoggedInUserInfor(token);
                var usermodel = new UserModel();
                usermodel.Id = loggedUser.Id;
                usermodel.Email = loggedUser.Email;
                var profile = await _profileEndPoint.GetProfile(usermodel);
                var consumerModel = new ConsumerModel();
                consumerModel.ProfileId = profile.Id;
                consumerModel.Activation = 1;
                await _consumerEndPoint.PostConsumer(consumerModel);
                TempData["newuser"] = "Customer";

                return RedirectToAction("SignIn", "Profile");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        //Get OTP
        public async Task<ActionResult> GetOTP(OTPModel otmodel)
        {
            if (ModelState.IsValid)
            {
               
            }

            return View();
        }

        //Loggin out method
        public ActionResult Logout()
        {
            //clear the instances in the container
            Session["Username"] = null;
            Session["profilename"] = null;
            _loggedInUserModel = null;
           
            _apiHepler.LogOutuser();
            Session["log"] = null;
            _apiHepler = null;          
               return RedirectToAction("Index", "Service");
          
        }
    }
}