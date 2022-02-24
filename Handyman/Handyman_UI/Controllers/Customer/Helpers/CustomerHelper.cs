using Handyman_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Controllers.Customer.Helpers
{
    public class CustomerHelper: ICustomerHelper
    {
         bool _IsLoggedIn;
         bool _IsRegistered;
         bool _IsCustomer;
         bool _CanRequest;
         bool _CanCancelRequest;
         IServicesLoader _servicesLoader;
         List<ServiceModel> RequestedServices;


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
                if(!_IsLoggedIn)
                {
                    _CanRequest = false;
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
                if(!_IsRegistered)
                {
                    _IsLoggedIn = false;
                    _IsCustomer = false;
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
                if(!_IsCustomer)
                {
                    _CanRequest = false;
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
                return _CanRequest;
            }
            set 
            {
                _CanRequest = value;//notify property of change
            }
        }
        //******************************End Properties********************************//




        //This will be the only visible action for CustomerHelper
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