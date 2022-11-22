using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class ProviderHelper
    {
        static IServiceProviderEndPoint? _providerEndPoint;
        static ServiceProviderModel? providerModel;
        string? ErrorMsg;
        public ProviderHelper(IServiceProviderEndPoint providerEndPoint)
        {
            _providerEndPoint = providerEndPoint;

        }

        public async Task AddService(ServiceProviderModel provider)
        {
            try
            {
                await _providerEndPoint.AddService(provider);
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }


    }
}
