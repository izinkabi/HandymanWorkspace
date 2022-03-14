using HandymanDataLibrary.Internal;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{

    public class ProfilesController : ApiController
    {
        private ProfileData profileData;
        private ProfileModel profileModel;
        private List<ProfileModel.AddressModel> addresses;


        //Posting a profile 
        [Route("api/PostProfile")]
        public void PostProfile(ProfileModel profile)
        {
            profileData = new ProfileData();
            profileData.PostProfile(profile);
        }
        // Get a profile by id
        public ProfileModel GetProfileById(string userId)
        {

            profileData = new ProfileData();
            profileModel = profileData.GetProfileByUserId(userId);
            //var tempProfile = new ProfileModel();

            return profileModel;
        }
        //Updating a profile
        [Route("api/UpdateProfile")]
        public void UpdateProfile(ProfileModel profile)
        {
           // profileData = new ProfileData();
            profileData.UpdateProfile(profile);
        }

        //Getting addresses by City
        [Route("api/GetAddressesByCiy")]
        public List<ProfileModel.AddressModel> GetAddressesByCiy(string City)
        {
            profileData = new ProfileData();
            addresses = profileData.GetAddressesByCity(City);
            return addresses;
        }
        //Getting addresses by Postal code
        [Route("api/GetAddressesByPostalCode")]
        public List<ProfileModel.AddressModel> GetAddressesByPostalCode(int PostalCode)
        {
            profileData = new ProfileData();
            addresses = profileData.GetAddressesByPostalCode(PostalCode);
            return addresses;
        }
        //Getting addresses by Surburb
        [Route("api/GetAddressesBySurburb")]
        public List<ProfileModel.AddressModel> GetAddressesBySurburb(string Surburb)
        {
            addresses =  profileData.GetAddressesBySurburb(Surburb);
            return addresses;
        }

        
    }
}