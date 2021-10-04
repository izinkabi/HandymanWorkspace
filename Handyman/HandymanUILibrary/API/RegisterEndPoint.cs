using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class RegisterEndPoint
    {
        private APIHelper _apiClient;
        private string email;
        public RegisterEndPoint(APIHelper aPIHelper)
        {
            _apiClient = aPIHelper;
        }


        public async Task<NewUserModel> RegisterUser(NewUserModel newUser)
        {

            email = newUser.Email;

            using (HttpResponseMessage httpResponse = await _apiClient.ApiClient.PostAsJsonAsync("/api/Account/Register", newUser))
            {
                if (httpResponse.IsSuccessStatusCode)
                {


                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }

            }


        }

        public async Task<NewUserModel> SaveNewUser(NewUserModel userModel)
        {
            // var data = new FormUrlEncodedContent(new[]
            //{

            //     new KeyValuePair<string, string>("email", email),

            // });
           // _apiClient = new APIHelper();
          
            
                HttpResponseMessage httpResponse = await _apiClient.ApiClient.PostAsJsonAsync("/api/Users", new { email = email });
            
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }

            

        }

    }
}
