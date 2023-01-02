using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class BusinessHelper : IBusinessHelper
    {
        static IBusinessEndPoint _business;
        static AuthenticationStateProvider? _authenticationStateProvider;
        string? userId;
        IServiceProviderEndPoint _providerEndPoint;
        ServiceProviderModel? provider;

        public BusinessHelper(IBusinessEndPoint business, AuthenticationStateProvider? authenticationStateProvider, IServiceProviderEndPoint providerEndPoint)
        {
            _business = business;
            _authenticationStateProvider = authenticationStateProvider;
            _providerEndPoint = providerEndPoint;
        }

        //Get the logged in User Id
        async Task<string>? GetUserId()
        {
            try
            {
                if (userId == null)
                {
                    var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                    userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

                }
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get the business with the logged in user
        public async Task<BusinessModel>? GetBusinessLoggedInEmployee()
        {
            try
            {
                BusinessModel? business = new()!;
                if (userId == null)
                {
                    userId = await GetUserId();
                }

                if (userId != null)
                {
                    business = await _business.GetLoggedInEmployee(userId);
                }
                return business;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        async Task IBusinessHelper.CreateBusiness(BusinessModel newBiz)
        {
            if (newBiz != null)
            {
                try
                {
                    await _business.CreateNewBusiness(StampBusinessUserAsync(newBiz).Result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }

        async Task<BusinessModel>? StampBusinessUserAsync(BusinessModel? newBiz)
        {
            if (newBiz != null)

                newBiz.date = DateTime.Now;

            newBiz.Employee.employeeId = await GetUserId();
            newBiz.Employee.pro_providerId = await GetUserId();
            newBiz.registration.businessType = newBiz.Type;
            newBiz.registration.name = newBiz.Name;
            newBiz.Employee.employeeProfile.UserId = newBiz.Employee.employeeId;
            newBiz.address.add_country = "Sout Africa";

            return newBiz;

        }

        public async Task<ServiceProviderModel>? GetProvider()
        {
            if (userId == null)
            {
                await GetUserId();
            }

            try
            {
                if (userId != null)
                    provider = await _providerEndPoint.GetProvider(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return provider;
        }

        //Get the employee Id hence the provider Id (stamping the request)
        public async Task<RequestModel> StampNewRequest(RequestModel newRequest)
        {
            if (userId == null)
            {
                userId = await GetUserId();
            }
            if (newRequest != null && userId != null)
            {
                newRequest.req_employeeid = userId;
            }
            return newRequest;

        }


    }
}
