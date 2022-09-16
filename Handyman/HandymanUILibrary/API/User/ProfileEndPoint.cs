using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ProfileEndPoint/*: IProfileEndPoint*/
    {

        private IAPIHelper _aPIHelper;
        
       // private List<ProviderAddress> addresses;
       
        
        public ProfileEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
            
        }

        //a Profile Post endpoint
        //public async Task<ProfileModel> PostProfile(ProfileModel profile)
        //{
          

        //    HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/PostProfile", profile);
            
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var result = await responseMessage.Content.ReadAsAsync<ProfileModel>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(responseMessage.ReasonPhrase);
        //        }
        //}

      
        //Getting a profile endpoint
        //public async Task<ProfileModel> GetProfile(string id)
        //{


        //    using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/Profiles?userId={id}"))
        //    {
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {

        //            var result = await httpResponseMessage.Content.ReadAsAsync<ProfileModel>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }

        //}
        
        //Only invoked by the request 
        //Get the profile by id
        //public async Task<ProfileModel> GetProfileById(int Id)
        //{


        //    using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/Profiles?Id={Id}"))
        //    {
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {

        //            var result = await httpResponseMessage.Content.ReadAsAsync<ProfileModel>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }

        //}

        //Updating a profile
        //public async Task UpdateProfile(ProfileModel profile)
        //{
        //    HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("api/UpdateProfile", profile);

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var result = await responseMessage.Content.ReadAsAsync<ProfileModel>();
              
        //    }
        //    else
        //    {
        //        throw new Exception(responseMessage.ReasonPhrase);
        //    }
        //}

        //Getting address in the given City
        //public async Task<List<ProviderAddress>> GetAddressesByCiy(string City)
        //{
        //    using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetAddressesByCity?City={City}"))
        //    {
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {

        //            addresses = await httpResponseMessage.Content.ReadAsAsync<List<ProviderAddress>>();
        //            return addresses;
        //        }
        //        else
        //        {
        //            throw new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }
        //}

        //Returning a list of Addresses by Surburb
        //public async Task<List<ProviderAddress>> GetAddressesBySurburb(string Surburb)
        //{
        //    using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetAddressesBySurburb?Surburb={Surburb}"))
        //    {
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {

        //            addresses = await httpResponseMessage.Content.ReadAsAsync<List<ProviderAddress>>();
        //            return addresses;
        //        }
        //        else
        //        {
        //            throw new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }
        //}

        //Getting a list of addresses in the given Postal Code
        //public async Task<List<ProviderAddress>> GetAddressesByPostalCode(int PostalCode)
        //{
        //    using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetAddressesByPostalCode?PostalCode={PostalCode}"))
        //    {
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {

        //            addresses = await httpResponseMessage.Content.ReadAsAsync<List<ProviderAddress>>();
        //            return addresses;
        //        }
        //        else
        //        {
        //            throw new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }
        //}

    }
}
