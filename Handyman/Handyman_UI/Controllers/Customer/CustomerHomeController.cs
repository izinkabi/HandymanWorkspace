using Handyman_UI.Controllers.Customer.Helpers;
using Handyman_UI.Controllers.Requests.Helpers;

using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Handyman_UI.Controllers
{
    public sealed class CustomerHomeController : LoginController
    {

         CustomerHelper Helper;
         RequestHelper RequestHelper;   
         IConsumerEndPoint _consumerEndPoint;
         IRequestEndPoint _requestEndPoint;
         IServiceProviderEndPoint _serviceProviderEndPoint;
         ConsumerModel customer;
         string ErrorMsg;
        

        //The use of constructor allows for the Imterfaces to be rendered
        public CustomerHomeController(IAPIHelper aPIHelper,IProfileEndPoint profileEndpoint,
            IConsumerEndPoint consumerEndpoint,IRegisterEndPoint registerEndPoint,
            IloggedInUserModel LoggedInUserModel,IServicesLoader servicesLoader,IRequestEndPoint
            requestEndPoint,IServiceProviderEndPoint serviceProviderEndPoint)
            :base(aPIHelper,profileEndpoint,registerEndPoint,LoggedInUserModel)
        {
            
            _consumerEndPoint = consumerEndpoint;
            Helper = new CustomerHelper(servicesLoader);
           
            _requestEndPoint = requestEndPoint;
            _serviceProviderEndPoint = serviceProviderEndPoint;
        }


        public async Task<ActionResult> Home()
        {
                try
                {
                    profileModel = (ProfileModel)Session["loggedinprofile"];//Get the logged in profile
                    if (profileModel is ProfileModel)
                        Helper.IsLoggedIn = true;
                    customer = await _consumerEndPoint.GetConsumerByProfileId(profileModel.Id);//getting the customer. This can start a customer session
                    if (customer is ConsumerModel)
                        Helper.IsCustomer = true;
                        return RedirectToAction("Index", "Service");
                }
                catch (Exception ex)
                {
                    //abort the final stage of login
                    customer = null;
                    ErrorMsg = ex.Message;
                    return RedirectToAction("SignIn", "LogIn");
                }
        }
            //Home page Action funtion
        
        public ActionResult UserDashBoard()
        {
            return View();
        }

        //this method invokes a request 
        public ActionResult CustomerServiceRequest(ConsumerModel consumer,ServiceModel service)
        {
            if (Helper.IsCustomer)
            {
                RequestHelper = new RequestHelper(_requestEndPoint, _serviceProviderEndPoint, _profileEndPoint);//A customer has a request relationship
            }
            else if(consumer is ConsumerModel && consumer != null)
            {
                Helper.IsCustomer = true;
                
                CustomerServiceRequest(consumer, service);
            }
            return View("Details","Requests");
        }

        //Starting the process
        private ActionResult SignIn()
        {
            if (Session.IsCookieless)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                //this is where we use the cookie of the session
                //to keep the customer logged in
                return View();
            }
        }

        //Register a new customer
        public async Task<RedirectToRouteResult> RegisterCustomer()
        {
            try
            {

                profileModel = (ProfileModel)Session["loggedinprofile"];

                customer = new ConsumerModel();
                customer.Activation = 1;
                customer.ProfileId = profileModel.Id;

                await _consumerEndPoint.PostConsumer(customer);
                Helper.IsCustomer = true;
                TempData["newuser"] = "Customer";

                return RedirectToAction("Index", "Service");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        
      
    }
}