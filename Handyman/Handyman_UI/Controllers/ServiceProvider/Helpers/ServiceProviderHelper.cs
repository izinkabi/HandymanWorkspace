using HandymanUILibrary.API;
using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Handyman_UI.Controllers.ServiceProvider.Helpers
{
    public class ServiceProviderHelper
    {
        IServiceProviderEndPoint _serviceProviderEndPoint;
        string ErrorMsg;

        static bool _IsServiceProvider,_IsLoggedIn,_CanProvideService, _IsAvailable,_CanCloseRequest;

        /// <summary>
        /// Constractor for the Service Provider's Instance of an interface
        /// </summary>
        /// <param name="serviceProviderEndPoint"></param>
        public ServiceProviderHelper(IServiceProviderEndPoint serviceProviderEndPoint)
        {
            _serviceProviderEndPoint = serviceProviderEndPoint;
           

        }

       /// <summary>
       /// Property is used to check is service is availble 
       /// </summary>
        public bool IsAvailable 
        {
            get
            {
                return _IsAvailable;
            }
            set
            {
                _IsAvailable = value;
            }
        }
        /// <summary>
        /// Property used to check if service provider is available
        /// </summary>
        public bool IsServiceProvider 
        {
            get
            {
                if (_IsServiceProvider)
                {
                    _IsAvailable = true;
                }
                return _IsServiceProvider;
            }
            set
            {
                _IsServiceProvider = value;
            }
        }
        /// <summary>
        /// This Property is used to check is a request can be closed
        /// </summary>
        public bool CanCloseRequest 
        {
            get
            {
                return _CanCloseRequest;
            }
            set
            {
                _CanCloseRequest = value;
            }
        }

       /// <summary>
       /// This function is used to Start a request
       /// </summary>
       /// <returns></returns>
        async Task GetRequests()
        {
            //try
            //{
            //    RequestModels = await _requestEndPoint.GetRequests();
            //}
            //catch (Exception ex)
            //{
            //    ErrorMsg = ex.Message;
            //}
           
        }
       /// <summary>
       /// This function is used to start a service 
       /// </summary>
        void StartService()
        {
            /**Start the service*/
            /**Update**/
            if (IsServiceProvider)
            {
                if (IsAvailable)
                {

                }
            }
        }
        /// <summary>
        /// This function is used to cancel and close a request
        /// </summary>
        void FinishService()
        {
            if (_CanCloseRequest)
            {

            }
        }
        /// <summary>
        /// This validation method is used to Accept or Decline a request
        /// </summary>
        /// <returns></returns>
        async Task AcceptServiceRequest()
        {

        }


    }
}