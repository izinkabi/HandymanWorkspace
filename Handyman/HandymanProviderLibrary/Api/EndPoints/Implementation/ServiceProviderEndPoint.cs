using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation
{
    public class ServiceProviderEndPoint : EmployeeEndPoint, IServiceProviderEndPoint
    {

        static IAPIHelper? _apiHelper;
        ServiceProviderModel? serviceProvider;

        public ServiceProviderEndPoint(IAPIHelper apiHelper) : base(apiHelper)
        {
            _apiHelper = apiHelper;
        }
        //Add service(s) of business's provider
        async Task IServiceProviderEndPoint.AddService(ServiceProviderModel provider)
        {
            try
            {
                await _apiHelper.ApiClient.PostAsJsonAsync("/api/AddServices", provider);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Remove the service of a provider
        async Task IServiceProviderEndPoint.RemoveService(ServiceModel service, string providerId)
        {
            try
            {
                await _apiHelper.ApiClient.DeleteAsync($"/api/Delivery/Delete?Id={service.id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        //Adding a new service provider
        async Task IServiceProviderEndPoint.CreateServiceProvider(ServiceProviderModel provider)
        {
            try
            {
                await _apiHelper.ApiClient.PostAsJsonAsync("/api/Delivery/Business/AddMember", provider);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get a service provider of the given ID
        public async Task<ServiceProviderModel> GetProvider(string userId)
        {
            try
            {
                ServiceProviderModel sp = await _apiHelper.ApiClient.GetFromJsonAsync<ServiceProviderModel>($"/api/Delivery/Business/GetProvider?employeeId={userId}");

                return sp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
