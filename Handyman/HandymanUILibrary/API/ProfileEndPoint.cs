using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ProfileEndPoint
    {

        private APIHelper _aPIHelper;
        public ProfileModel profileModel;
        //Initializing
        public ProfileEndPoint(APIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        //a Profile Post endpoint
        public async Task<ProfileModel> PostProfile(ProfileModel profile)
        {
            using (HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/Profiles", profile))
            {
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

        }

        //Getting a profile endpoint
        //This might use a different model called getProfileModel
        public async Task<ProfileModel> GetProfile()
        {
           
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/Profile"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                 
                    
                    profileModel = new ProfileModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<ProfileModel>();
                    
                    profileModel.UserId = result.UserId;
                    profileModel.Name = result.Name;
                    profileModel.Type = "user";
                    profileModel.Surname = result.Surname;
                    profileModel.DateofBirth = result.DateofBirth;
                    profileModel.HomeAddress = result.HomeAddress;
                    profileModel.PhoneNumber = result.PhoneNumber;

                    return profileModel;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }


    }
}
