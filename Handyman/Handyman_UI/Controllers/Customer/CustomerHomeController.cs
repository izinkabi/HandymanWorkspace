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
         private ConsumerModel customer;
         string ErrorMsg;
        

        //The use of constructor allows for the Imterfaces to be rendered
        public CustomerHomeController(IAPIHelper aPIHelper,IProfileEndPoint profileEndpoint,
            IConsumerEndPoint consumerEndpoint,IRegisterEndPoint registerEndPoint,
            IloggedInUserModel LoggedInUserModel,IServicesLoader servicesLoader,IRequestEndPoint
            requestEndPoint,IServiceProviderEndPoint serviceProviderEndPoint)
            :base(aPIHelper,profileEndpoint,registerEndPoint,LoggedInUserModel)
        {
            
            _consumerEndPoint = consumerEndpoint;
            Helper = new CustomerHelper(servicesLoader,profileEndpoint);
           
            _requestEndPoint = requestEndPoint;
            _serviceProviderEndPoint = serviceProviderEndPoint;
        }

        //The home page method of the customer
        public async Task<ActionResult> Home()
        {
                try
                {
                if (Session["loggedinprofile"] != null)
                    profileModel = (ProfileModel)Session["loggedinprofile"];
                    //profileModel = (ProfileModel)Session["loggedinprofile"];//Get the logged in profile
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

        //Starting the process method/Authenticate and Authorize in the api
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

        //Register a new customer method
        public async Task<RedirectToRouteResult> RegisterCustomer()
        {
            try
            {

                profileModel = (ProfileModel)Session["loggedinprofile"];
                if(profileModel!=null)
                customer = new ConsumerModel();
                customer.Activation = 1;
                customer.ProfileId = profileModel.Id;

                await _consumerEndPoint.PostConsumer(customer);
                Helper.IsCustomer = true;
                TempData["newuser"] = profileModel.Name + " " + profileModel.Surname;

                return RedirectToAction("Index", "Service");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        
      
    }
}