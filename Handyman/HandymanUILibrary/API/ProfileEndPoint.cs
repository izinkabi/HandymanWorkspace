using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ProfileEndPoint: IProfileEndPoint
    {

        private IAPIHelper _aPIHelper;
        private  ProfileModel profileModel;
        private IloggedInUserModel _loggedInUserModel;
        //Initializing
        public ProfileEndPoint(IAPIHelper aPIHelper, IloggedInUserModel loggedInUserModel)
        {
            _aPIHelper = aPIHelper;
            _loggedInUserModel = loggedInUserModel;
        }

        //a Profile Post endpoint
        public async Task<ProfileModel> PostProfile(ProfileModel profile)
        {
          

            HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/Profiles", profile);
            
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<ProfileModel>();
                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            

        }

      

        

        //Getting a profile endpoint
        public async Task<ProfileModel> GetProfile(UserModel user)
        {


            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/Profiles?userId={user.Id}"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    var result = await httpResponseMessage.Content.ReadAsAsync<ProfileModel>();
                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }


    }
}
