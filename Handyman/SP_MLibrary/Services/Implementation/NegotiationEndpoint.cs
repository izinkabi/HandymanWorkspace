using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Json;

namespace SP_MLibrary.Services.Implementation
{
    public class NegotiationEndPoint : INegotiationEndPoint
    {
        private readonly IServiceHelper _ServicesHelper;

        public NegotiationEndPoint(IServiceHelper ServicesHelper)
        {
            _ServicesHelper = ServicesHelper;
        }

        //Get a negotiation model
        public async Task<NegotiationModel> GetNego(int negoId)
        {
            if (negoId is 0) return null;
            try
            {
                NegotiationModel negotiation = await _ServicesHelper.ServicesClient.GetFromJsonAsync<NegotiationModel>($"/Services/negos/getnego?negoId={negoId}");
                return negotiation;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Insert a new Nego
        public async Task<bool> InsertNego(NegotiationModel negotiation)
        {
            if (negotiation is null) return false;
            try
            {
                var result = await _ServicesHelper.ServicesClient.PostAsJsonAsync("/Services/negos/postnego", negotiation);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Update a negotiation
        public async Task<bool> UpdateNego(NegotiationModel negotiation)
        {
            if (negotiation is null) return false;
            try
            {
                var httpResponse = await _ServicesHelper.ServicesClient.PutAsJsonAsync("/Services/negos/UpdateNego", negotiation);
                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
