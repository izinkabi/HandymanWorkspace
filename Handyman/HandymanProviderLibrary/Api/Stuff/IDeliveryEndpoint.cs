using HandymanProviderLibrary.Models.Delivery;

namespace HandymanProviderLibrary.Api.Stuff
{
    public interface IDeliveryEndpoint
    {
        Task<EmployeeModel> GetEmployeeWithRatings(string employeeId);
    }
}