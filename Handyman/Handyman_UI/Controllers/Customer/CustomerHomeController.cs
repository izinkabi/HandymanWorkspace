using Handyman_UI.Controllers.Customer.Helpers;
using Handyman_UI.Controllers.Requests.Helpers;
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

         CustomerHelper Helper;
         RequestHelper RequestHelper;
         IAPIHelper _apiHepler;
         IProfileEndPoint _profileEndPoint;
         IConsumerEndPoint _consumerEndPoint;
         IRequestEndPoint _requestEndPoint;
         IServiceProviderEndPoint _serviceProviderEndPoint;
        //private IRegisterProviderEndPoint _registerProviderEndpoint;
       
        
        

        //The use of constructor allows for the Imterfaces to be rendered
        public CustomerHomeController(IAPIHelper aPIHelper,IProfileEndPoint profileEndpoint,
            IConsumerEndPoint consumerEndpoint,IServicesLoader servicesLoader,IRequestEndPoint requestEndPoint,IServiceProviderEndPoint serviceProviderEndPoint)
        {
            _apiHepler = aPIHelper;
            _profileEndPoint = profileEndpoint;
            _consumerEndPoint = consumerEndpoint;
            Helper = new CustomerHelper(servicesLoader);
            Helper.IsLoggedIn = true;
            _requestEndPoint = requestEndPoint;
            _serviceProviderEndPoint = serviceProviderEndPoint;
        }

        

        //Home page Action funtion

        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult RequestAService()
        {
            if (Helper.IsCustomer)
            {

                RequestHelper = new RequestHelper(_requestEndPoint, _serviceProviderEndPoint, _profileEndPoint);
            }
               return View("Details","Requests");
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
                Helper.IsCustomer = true;
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