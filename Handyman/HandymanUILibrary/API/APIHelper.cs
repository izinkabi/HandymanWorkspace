using HandymanUILibrary.Models;
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
     public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient;
        private IloggedInUserModel _loggedInUserModel;

        public APIHelper(IloggedInUserModel loggedInUser)
        {
            InitializeCLient();
            _loggedInUserModel = loggedInUser;  
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
        public async Task GetLoggedInUserInfor(string Token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

            using (HttpResponseMessage httpResponseMessage = await _apiClient.GetAsync("/api/Users"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                   
                    var result = await httpResponseMessage.Content.ReadAsAsync<loggedInUserModel>();
                    _loggedInUserModel.Token = Token;
                    _loggedInUserModel.Id = result.Id;
                    _loggedInUserModel.Username = result.Username;
                    _loggedInUserModel.Email = result.Email;
                    _loggedInUserModel.CreateDate = result.CreateDate;
                //    _loggedInUserModel.FirstName = result.FirstName;
                //    _loggedInUserModel.LastName = result.LastName;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
           

        }


       

        public void LogOutuser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }

       
    }
}
