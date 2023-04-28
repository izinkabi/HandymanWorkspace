using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers
{
    [Route("api/negos")]
    [ApiController]
    public class NegosController : ControllerBase
    {
        private readonly INegotiationData _negoData;

        public NegosController(INegotiationData negoData)
        {
            _negoData = negoData;
        }

        // GET api/<NegosController>/5
        [HttpGet]
        [Route("GetNego")]
        public NegoModel GetNego(int negoId)
        {
            try
            {
                return _negoData.GetNegotiation(negoId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        // POST api/<NegosController>
        [HttpPost]
        [Route("PostNego")]
        public void PostNego([FromBody] NegoModel value)
        {
            try
            {
                _negoData.InsertNego(value);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // PUT api/<NegosController>/5
        [HttpPut]
        [Route("UpdateNego")]
        public void UpdateNego([FromBody] NegoModel value)
        {
            try
            {
                _negoData.UpdateNego(value);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<NegosController>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
