﻿using HandymanUILibrary.Models.Auth;
using HandymanUILibrary.Models.AuthModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.User
{
    public class APIHelper : IAPIHelper
    {

        private HttpClient _apiClient;
        private readonly IConfiguration _configuration;

        public APIHelper(IConfiguration configuration)
        {
            _configuration = configuration;
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
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(_configuration["ApiUrl"]);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/*+json"));
        }
        /// <summary>
        ///authenticate the user by passing a username and password to the token endpoint and return a authenticate user with a token
        /// </summary>
        /// <returns>logged in user model</returns>
        public async Task<string> AuthenticateUser(string username, string password)
        {

            LoginModel? data = new LoginModel
            {
                Email = username,
                Password = password

            };

            using (HttpResponseMessage httpResponse = await _apiClient.PostAsJsonAsync<LoginModel>("Auth/login", data))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var token = await httpResponse.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }
            }

        }

        //Overloading AuthenticateUser method to take a string of a user ID
        public async Task<string> AuthenticateUser(string userId)
        {
            try
            {
                LoginModel? data = new LoginModel
                {
                    UserId = userId
                };
                InitializeCLient();
                using (HttpResponseMessage httpResponse = await _apiClient.PostAsJsonAsync<LoginModel>("Auth/login", data))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var token = await httpResponse.Content.ReadAsStringAsync();
                        return token;
                    }
                    else
                    {
                        return string.Empty;
                    }
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IloggedInUserModel> GetLoggedInUserInfor(string Token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

            using (HttpResponseMessage httpResponseMessage = await _apiClient.GetAsync("Auth/userprofile"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadFromJsonAsync<loggedInUserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }


        }

        //Log the user out of the system ***cheat!
        public async Task<bool> LogOutUser()
        {

            try
            {
                //_apiClient.DefaultRequestHeaders.Clear();

                var response = await _apiClient.PostAsJsonAsync("auth/logout", new { });
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}