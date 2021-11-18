using HandymanDataLibrary.Internal;
using HandymanDataLibrary.Models;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace HandymanAPI.Controllers
{
    public class ProfilesController : ApiController
    {


        public void PostProfile(ProfileModel profile)
        {
            ProfileData data = new ProfileData();
            data.PostProfile(profile);
        }

        
        // GET: Profiles
        public ProfileModel GetProfileById(int Id)
        {
            ProfileData data = new ProfileData();
            return data.GetProfileById(Id);
        }

        //POST: Profile

        
    }
}