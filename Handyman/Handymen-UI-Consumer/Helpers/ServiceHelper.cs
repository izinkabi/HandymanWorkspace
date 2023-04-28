using HandymanUILibrary.API.Services;
using HandymanUILibrary.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public class ServiceHelper
    {
        private readonly IServiceEndPoint _serviceEndPoint;

        public ServiceHelper(IServiceEndPoint serviceEndPoint)
        {
            _serviceEndPoint = serviceEndPoint;
        }

        public async Task<PriceModel> GetPrice(int priceId)
        {
            try
            {
                if (priceId is 0)
                {
                    return null;
                }
                PriceModel price = await _serviceEndPoint.GetPrice(priceId);
                return price;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
