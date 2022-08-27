using HandymanProviderLibrary.Api.Service;
using HandymanProviderLibrary.Models;
using HandymanUILibrary.API.Consumer;
using HandymanUILibrary.API.ServiceProvider;

namespace Handyman_UI_Provider.Hubs.ServiceDelivery
{
    //This is the Helper/Mediator class for Request and Order Hubs
    //in internal communication of hubs and any API calls
    public class ServiceDelivery : IServiceDelivery
    {
        //These injected service will prompt us to add a SignalRApp's library for modula code reasons 
        private IServiceEndpoint _serviceEndPoint;
        private IOrderEndPoint _orderEndPoint;
        private IRequestEndPoint _requestEndPoint;

        public ServiceDelivery(IServiceEndpoint serviceEndPoint, IOrderEndPoint orderEndPoint, IRequestEndPoint requestEndPoint)
        {
            _serviceEndPoint = serviceEndPoint;
            _orderEndPoint = orderEndPoint;
            _requestEndPoint = requestEndPoint;
        }

        //API call to filter the service providers by a service  they provide
        public async Task<List<ProviderServiceModel>> FilterServiceProvidersByService(int id)
        {
            var provider = await _serviceEndPoint.GetProviderServicesByServiceId(id);
            return provider;
        }

        //API call for order CRUID 
        //API call for Request CRUID
        //And updating of Order uses request

        //*****Calling the google api service for filtering providers
        //by nearest and mapping them with consumer as origin****

    }
}
