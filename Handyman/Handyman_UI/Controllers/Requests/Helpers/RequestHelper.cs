
using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Handyman_UI.Controllers.Requests.Helpers
{
    public class RequestHelper
    {
        bool _IsValideRequest;
        bool _IsAvailableProvider;
        bool _CanPostRequest;
        bool _CanCancelRequest;

        IRequestEndPoint _requestEndPoint;
        IServiceProviderEndPoint _serviceProviderEndPoint;
        IProfileEndPoint _profileEndPoint;

        List<ServiceProviderModel> serviceProviders;
        List<ProfileModel.AddressModel> ServiceProviderAddresses;
        RequestModel Requst;
        string _ErrorMsg;

        public RequestHelper(IRequestEndPoint requestEndPoint,IServiceProviderEndPoint serviceProviderEndPoint,IProfileEndPoint profileEndPoint)
        {
            _requestEndPoint = requestEndPoint;
            _serviceProviderEndPoint = serviceProviderEndPoint;
            _profileEndPoint = profileEndPoint;
        }

        //****************Properties******************//
        public string ErrorMsg 
        {
            get
            {
                return _ErrorMsg;
            }
        }
        public bool IsValideRequest 
        {
            get
            {
                //Check for the conditions of a valid request
                return _IsValideRequest;
            }
            set
            {
                _IsValideRequest = value;//Alter the state of the request
            }
        }

       

        public bool IsProviderAvailable
        {
            get { return _IsAvailableProvider; }
            set { IsProviderAvailable = value; }
        }

        public bool CanPostRequest 
        {
            get
            {
                return _CanPostRequest;
            }
            set
            {
                _CanPostRequest = value;
            }
        }

        

        public bool CanCancelRequest
        {
            get { return _CanCancelRequest; }
            set { _CanCancelRequest = value; }
        }
        //******************End of Properties*************//




        //This is the only accesseble method outside the
        //class's scope 
        //It passes the parameters required to initiate a request
        //as a component
        public void RequestService(UserModel user,ServiceModel service)
        {
            
        }
       

        //Populating the local providers
        async Task GetProviders()
        {
            serviceProviders = await _serviceProviderEndPoint.GetServiceProviders();
            
        }


        async Task PostRequest()
        {
            if (_CanPostRequest)
            {
                try
                {
                   // await _requestEndPoint.PostRequest(Requst);//This should be the only posting of the request
                }catch(Exception ex)
                {
                    _ErrorMsg = ex.Message;
                }
            }
        }

        //This will require Google Maps Api
        //Pass the origin and destination addresses 


        //The closest address of service provider will be used as the
        //the request attribute providers' service id
        void GetCloserProvider()
        {

        }

        //This method allows for the making of the request
        //as the actual combination of customer, provider(Handyman) by a service
        void AssignServiceToProvider()
        {
            if (_IsAvailableProvider)
            {

            }
        }

        //Stopping the request will be dependant on both the customer and 
        //the provider's delegations
        void CancelRequest()
        {
            if (_CanCancelRequest)
            {

            }
        }
    }
}