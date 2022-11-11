using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models.Delivery;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.Stuff
{
    public class DeliveryEndpoint : IDeliveryEndpoint
    {
        IAPIHelper _apiHelper;

        public DeliveryEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<EmployeeModel> GetEmployeeWithRatings(string employeeId)
        {
            try
            {
               EmployeeModel httpresponse = await _apiHelper.ApiClient.GetFromJsonAsync<EmployeeModel>($"api/Delivery/GetEmployee?employeeid={employeeId}");
                return httpresponse;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
