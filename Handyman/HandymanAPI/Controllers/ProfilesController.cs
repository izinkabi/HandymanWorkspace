﻿using HandymanDataLibrary.Internal;
using HandymanDataLibrary.Models;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HandymanAPI.Controllers
{
    [Authorize]
    public class ProfilesController : ApiController
    {
        [Authorize(Roles = "Customer,ServiceProvider,Admin")]
        public void PostProfile(ProfileModel profile)
        {
            ProfileData data = new ProfileData();
            data.PostProfile(profile);
        }


        // GET: Profiles
        [Authorize(Roles = "Customer,ServiceProvider,Admin")]
        public ProfileModel GetProfileById(string userId)
        {
            
            ProfileData data = new ProfileData();
            var dbProfile = data.GetProfileByUserId(userId);
            var tempProfile = new ProfileModel();

            return dbProfile;
        }

        //POST: Profile

        
    }
}