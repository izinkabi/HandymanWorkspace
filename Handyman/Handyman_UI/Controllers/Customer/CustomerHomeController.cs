﻿using Handyman_UI.Controllers.Customer.Helpers;
using Handyman_UI.Controllers.Requests.Helpers;

using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// The use of constructor allows for the Imterfaces to be rendered
        /// </summary
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

        private ActionResult RedirectToAction(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Home page Action funtion
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDashBoard()
        {
            return View();
        }
        /// <summary>
        /// this method invokes a request 
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="service"></param>
        /// <returns></return>
        //Searching addresses by City
        public async Task<ActionResult> SearchProvidersByCity(string City)
        {
            if (City != null)
            {

                List<ProviderAddress> addresses = new List<ProviderAddress>();
                var searchResults = new List<ProviderAddress>();
                try
                {
                    addresses = await Helper.GetAddressesByCity(City);

                    foreach (var address in addresses)
                    {
                        if (address.City.Contains(City))
                        {
                            searchResults.Add(address);

                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;

                }
                return View("ProviderSearchByCity", new { Addresses = addresses });
            }
            return View();
        }
        //Searching addresses by Postal Code
        public async Task<ActionResult> SearchProvidersByPostalCode(int PostalCode)
        {
            if (PostalCode != 0)
            {

                List<ProviderAddress> addresses = new List<ProviderAddress>();
                var searchResults = new List<ProviderAddress>();
                try
                {
                    addresses = await Helper.GetAddressesByPostalCode(PostalCode);

                    foreach (var address in addresses)
                    {
                        if (address.City.Equals(PostalCode))
                        {
                            searchResults.Add(address);

                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;

                }
                return View( addresses );
            }
            return View();
        }
        //Searching addresses by Surburb
        public async Task<ActionResult> SearchProvidersBySurburb(string Surburb)
        {
            if (Surburb != null)
            {

                List<ProviderAddress> addresses = new List<ProviderAddress>();
                var searchResults = new List<ProviderAddress>();
                try
                {
                    addresses = await Helper.GetAddressesBySurburb(Surburb);

                    foreach (var address in addresses)
                    {
                        if (address.City.Contains(Surburb))
                        {
                            searchResults.Add(address);

                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;

                }
                return View("ProviderSearchByCity", new { Addresses = addresses });
            }
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
        /// <summary>
        /// Starting the new signin process
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Register a new customer
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        //public async Task<RedirectToRouteResult> RegisterCustomer()
        //{
        //    try
        //    {

        //        profileModel = (ProfileModel)Session["loggedinprofile"];
        //        if(profileModel!=null)
        //        customer = new ConsumerModel();
        //        customer.Activation = 1;
        //        customer.ProfileId = profileModel.Id;

        //        await _consumerEndPoint.PostConsumer(customer);
        //        Helper.IsCustomer = true;
        //        TempData["newuser"] = profileModel.Name + " " + profileModel.Surname;

        //        return RedirectToAction("Index", "Service");
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
       
        
      
    }
}