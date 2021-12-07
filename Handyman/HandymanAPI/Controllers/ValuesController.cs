using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Authorize(Roles = "Customer")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Authorize(Roles = "ServiceProvider")]
        public string Get(int id)
        {
            return "Service Provider Role " + id; ;
        }

        // POST api/values
        [Authorize(Roles = "Customer,ServiceProvider")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [Authorize(Roles = "Customer,ServiceProvider,Admin")]
        public string Put(int id, [FromBody]string value)
        {
            if (User.IsInRole("Customer"))

            {
                return "Customer of "+value;

            }
            else if (User.IsInRole("Admin"))
            {
                return "Admin of "+value;
            }
            else if (User.IsInRole("ServiceProvider"))
            {
                return "Service Provider of " + value;
            }
            else
            {
                return "The End of "+value;
            }
            
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
