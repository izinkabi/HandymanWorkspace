using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.Request;

    public class RequestEndPoint : IRequestEndPoint
    {

        private IAPIHelper? _apiHelper;
        public RequestEndPoint(IAPIHelper? aPIHelper)
        {
            _apiHelper = aPIHelper; 
        }

        //Get the orders relavant to the provider
       public async Task<List<OrderModel>> GetAllOrdersByService(int serviceId)
        {
            List<OrderModel>? httpResponseMessage = new();

            try
            {
                httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/GetAllOrdersByService?serviceId={serviceId}");
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }     
            
        }

    }

