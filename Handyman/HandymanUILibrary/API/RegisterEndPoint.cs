using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class RegisterEndPoint: IRegisterEndPoint
    {
        private IAPIHelper _apiHelper;
        static private string email;
        static private IloggedInUserModel _loggedInUserModel;
        public RegisterEndPoint(IAPIHelper aPIHelper,IloggedInUserModel loggedInUserModel)
        {
            _apiHelper = aPIHelper;
            _loggedInUserModel = loggedInUserModel;
        }


        public async Task<IloggedInUserModel> RegisterUser(NewUserModel newUser)
        {

            var user = new FormUrlEncodedContent(new[]
          {

                 new KeyValuePair<string, string>("email", newUser.Email),
                 new KeyValuePair<string, string>("password", newUser.Password),
                 new KeyValuePair<string, string>("confirmPassword", newUser.ConfirmPassword),

           });

            using (HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsync("/api/Account/Register", user))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();
                    return _loggedInUserModel;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }

            }


        }

        public async Task<IloggedInUserModel> SaveNewUser(NewUserModel userModel)
        {
            var data = new FormUrlEncodedContent(new[]
           {

                 new KeyValuePair<string, string>("email", userModel.Email),

           });
           


            HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsync("/api/Users", data);
            
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();
                return _loggedInUserModel;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }

        }

    }
}
