using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Handyman_Api.Controllers
{
    [Route("api/Handymen")]
    [ApiController]
    public class HandymenController : ControllerBase
    {

        IProfileData _profileData;

        public HandymenController(IProfileData profileData)
        {
            _profileData = profileData;
        }

        // GET: api/<HandymenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HandymenController>/5
        [HttpGet]
        [Route("GetProfile")]
        public ProfileModel Get(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var profile = _profileData.GetProfile(userId);
                    if (profile != null) return profile;

                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<HandymenController>
        [HttpPost]
        [Route("PostProviderProfile")]
        public void PostProfile(ProfileModel profile)
        {
            if (profile != null)
            {
                _profileData.InsertProfile(profile);
            }
        }

        // PUT api/<HandymenController>/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HandymenController>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
