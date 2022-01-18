using Handyman_UI.Models;
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public class CustomerHomeController : Controller
    {

        private IAPIHelper _apiHepler;
        

        private IProfileEndPoint _profileEndPoint;
        private IConsumerEndPoint _consumerEndPoint;
        //private IRegisterProviderEndPoint _registerProviderEndpoint;
       
        
        

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

                return RedirectToAction("Index", "Service");
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
      
    }
}