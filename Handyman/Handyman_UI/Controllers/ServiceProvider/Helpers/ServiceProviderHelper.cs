﻿using HandymanUILibrary.API;
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
        IRequestEndPoint _requestEndPoint;
        List<RequestModel> RequestModels;
        string ErrorMsg;
        static bool _IsServiceProvider,_IsLoggedIn,_CanProvideService, _IsAvailable,_CanCloseRequest;


        public ServiceProviderHelper(IServiceProviderEndPoint serviceProviderEndPoint, IRequestEndPoint requestEndPoint)
        {
            _serviceProviderEndPoint = serviceProviderEndPoint;
            _requestEndPoint = requestEndPoint;

        }


        //****************The properties of the Service Provider Helper**********************************//
        public bool IsAvailable 
        {
            get
            {
                return _IsAvailable;
            } 
        }
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
        //*********************End of properties**********************//



        //Populate requests
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

        //Update the request in the database
        public async Task UpdateRequestStatus(RequestModel request)
        {          
            try
            {

                await _requestEndPoint.UpdateRequest(request);

            }catch(Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }

        //Start the service
        //Update
        void StartService()
        {

            if (IsServiceProvider)
            {
                if (IsAvailable)
                {

                }
            }
        }

        void FinishService()
        {
            if (_CanCloseRequest)
            {

            }
        }


        async Task AcceptServiceRequest()
        {

        }


    }
}