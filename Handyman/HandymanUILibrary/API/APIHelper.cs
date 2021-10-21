﻿using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
     public class APIHelper 
    {
        public HttpClient _apiClient;
        public loggedInUserModel _loggedInUserModel;
        public APIHelper()
        {
            InitializeCLient();
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }


        //We initialize the HTTP client and format the clients headings to pass the data as a json objet
        private void InitializeCLient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
        }
        /// <summary>
        ///authenticate the user by passing a username and password to the token endpoint and return a authenticate user with a token
        /// </summary>
        /// <returns>logged in user model</returns>
        public async Task<AuthenticatedUserModel> AuthenticateUser(string username, string password)
        {

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),

            });

            using (HttpResponseMessage httpResponse = await _apiClient.PostAsync("/token", data))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<AuthenticatedUserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }
            }   

        }
        public async Task<loggedInUserModel> GetLoggedInUserInfor(string Token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

            using (HttpResponseMessage httpResponseMessage = await _apiClient.GetAsync("/api/Users"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    //I'm so not sure about not using bootstraper because of alot of object instantiation 
                    _loggedInUserModel = new loggedInUserModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<loggedInUserModel>();
                    _loggedInUserModel.Token = Token;
                    _loggedInUserModel.Id = result.Id;
                    _loggedInUserModel.Username = result.Username;
                    _loggedInUserModel.Email = result.Email;
                    _loggedInUserModel.CreateDate = result.CreateDate;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return _loggedInUserModel;

        }


       

        public void LogOutuser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }

    }
}