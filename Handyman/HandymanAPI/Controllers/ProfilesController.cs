using HandymanDataLibrary.Internal;
using HandymanDataLibray.Models;
using System.Web.Http;

namespace HandymanAPI.Controllers
{

    public class ProfilesController : ApiController
    {
        [Route("api/PostProfile")]
        public void PostProfile(ProfileModel profile)
        {
            ProfileData data = new ProfileData();
            data.PostProfile(profile);
        }


        // GET: Profiles

        public ProfileModel GetProfileById(string userId)
        {
            
            ProfileData data = new ProfileData();
            var dbProfile = data.GetProfileByUserId(userId);
            //var tempProfile = new ProfileModel();

            return dbProfile;
        }

        //Update
        [Route("api/UpdateProfile")]
        public void UpdateProfile(ProfileModel profile)
        {
            ProfileData profileData = new ProfileData();
            profileData.UpdateProfile(profile);
        }

        
    }
}