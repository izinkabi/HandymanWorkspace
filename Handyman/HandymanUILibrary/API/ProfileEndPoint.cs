using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
           // var data = new FormUrlEncodedContent(new[]
           //{

           //      new KeyValuePair<string, string>("Name", profile.Name),
           //      new KeyValuePair<string, string>("Surname", profile.Surname),
           //      new KeyValuePair<string, string>("Home", profile.Name),
           //      new KeyValuePair<string, string>("Name", profile.Name),

           //});

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

        // this method saves the Address record in the database

        //public async Task<ProfileModel.AddressModel> PostAddress(ProfileModel.AddressModel address)
        //{
        //    using (HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/Profile", address))
        //    {
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var result = await responseMessage.Content.ReadAsAsync<ProfileModel.AddressModel>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(responseMessage.ReasonPhrase);
        //        }
        //    }
        //}

        //Getting a profile endpoint
        //This might use a different model called getProfileModel
        public async Task<ProfileModel> GetProfile()
        {
           
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/Profiles"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                 
                    
                    profileModel = new ProfileModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<ProfileModel>();
                    
                    profileModel.UserId = _loggedInUserModel.Id;
                    profileModel.Name = result.Name;
                    profileModel.Type = "user";
                    profileModel.Surname = result.Surname;
                    profileModel.DateofBirth = result.DateofBirth;
                    profileModel.Address = result.Address;
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
