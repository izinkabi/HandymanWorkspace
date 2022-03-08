using Handyman_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Controllers.Customer.Helpers
{
    public class CustomerHelper: Controller
    {
        static bool _IsLoggedIn;
        static bool _IsRegistered;
        static bool _IsCustomer;
         bool _CanRequest;
         bool _CanCancelRequest;
         IServicesLoader _servicesLoader;
         


        public CustomerHelper(IServicesLoader servicesLoader)
        {
            _servicesLoader = servicesLoader;
        }


        //*****************************************************//
        //The properties to set for the customers
        public bool IsLoggedIn 
        { 
            get 
            {
                while(Session["loggedinuser"]!=null)
                {
                    _IsLoggedIn = true;
                }
                return _IsLoggedIn;
            }
            set
            {
                _IsLoggedIn = value;
                //provide property of property change
            }
        }

        public bool IsRegistered 
        {
            get
            {
                if(_IsLoggedIn)
                {
                    _IsRegistered = true;
                   
                }
                return _IsRegistered;
            }
            set
            {
                _IsRegistered = value;
                //We need a notify of property change
            }
        }

        public bool IsCustomer 
        {
            get
            {
                if(_IsLoggedIn)
                {
                    
                    //this will be easy when a logged in customer is in a session with its user details and profile details.
                }
                return _IsCustomer;
            }
            set
            {
                _IsCustomer = value;
                //notify of property change
            }
        }

        public bool CanCancelRequest  
        {
            get 
            { 
                return _CanCancelRequest;
            }
            set 
            {
                _CanCancelRequest = value;
            }
        }


        public bool CanRequest 
        {
            get 
            {
                if (_IsLoggedIn && _IsCustomer)
                {
                    _CanRequest = true;
                }
                return _CanRequest;
            }
            set 
            {
                _CanRequest = value;//notify property of change
            }
        }
        //******************************End Properties********************************//




        public void LogOnVerification()
        {
            if (_IsLoggedIn)
            {
                return;
            }
            else{
                
            }
            
        }


        //Make the request with a customer and the a Service
        public void ActionARequest(CustomerModel customer ,ServiceModel service)
        {

            if (_IsLoggedIn)
            {
                if (_IsCustomer)
                {


                }          
            }
            
        }


    }
}