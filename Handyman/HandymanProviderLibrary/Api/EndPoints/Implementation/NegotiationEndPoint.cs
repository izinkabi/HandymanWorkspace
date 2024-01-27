using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation
{
    public class NegotiationEndPoint : INegotiationEndPoint
    {
        private readonly IAPIHelper _apiHelper;

        public NegotiationEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        //Get a negotiation model
        public async Task<NegotiationModel> GetNego(int negoId)
        {
            if (negoId is 0) return null;
            try
            {
                NegotiationModel negotiation = await _apiHelper.ApiClient.GetFromJsonAsync<NegotiationModel>($"/api/negos/getnego?negoId={negoId}");
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
                var result = await _apiHelper.ApiClient.PostAsJsonAsync<NegotiationModel>("/api/negos/postnego", negotiation);
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
                var httpResponse = await _apiHelper.ApiClient.PutAsJsonAsync<NegotiationModel>("/api/negos/UpdateNego", negotiation);
                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
