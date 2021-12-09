using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class RegisterEndPoint : IRegisterEndPoint
    {
        private IAPIHelper _apiHelper;
        static private string email;
        static private loggedInUserModel loggedInUserModel;
        public RegisterEndPoint(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;

        }


        public async Task<AuthenticatedUserModel> RegisterUser(NewUserModel newUser)
        {

            var user = new FormUrlEncodedContent(new[]
            {

                 new KeyValuePair<string, string>("email", newUser.Email),
                 new KeyValuePair<string, string>("password", newUser.Password),
                 new KeyValuePair<string, string>("confirmPassword", newUser.ConfirmPassword)

           });

            using (HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsync("/api/Account/Register", user))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();
                    var loggedInUser = await _apiHelper.AuthenticateUser(newUser.Email, newUser.Password);
                    await SaveNewUser(newUser.Email,newUser.UserRole);

                    return loggedInUser;
                }
                else
                {
                    throw new Exception(httpResponse.ReasonPhrase);
                }

            }


        }

            async Task SaveNewUser(string email,string userrole)
            {
                var data = new FormUrlEncodedContent(new[]
               {

                     new KeyValuePair<string, string>("email", email),
                     new KeyValuePair<string, string>("userrole",userrole)

               });

                

             HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsync("/api/Users", data);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var loggedInUserModel = new loggedInUserModel();
                    var result = await httpResponse.Content.ReadAsAsync<NewUserModel>();
                
                }
                    else
                    {
                        throw new Exception(httpResponse.ReasonPhrase);
                    }

            }

    }
}

