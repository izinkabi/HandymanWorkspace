using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class ProviderHelper : IProviderHelper
    {
        static IServiceProviderEndPoint? _providerEndPoint;
        ServiceProviderModel? providerModel;
        EmployeeModel? employeeModel;
        string? ErrorMsg;
        public ProviderHelper(IServiceProviderEndPoint providerEndPoint)
        {
            _providerEndPoint = providerEndPoint;

        }

        //Add new service
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

        //Get the loggedIn employee if they have an ID
        public async Task<ServiceProviderModel>? GetProvider(string userId)
        {
            if ((userId != null) && (employeeModel == null))
            {
                try
                {
                    providerModel = await _providerEndPoint.GetProvider(userId);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

            }
            return providerModel;
        }
    }
}
