using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IWorkshopEndPoint
    {
        Task<WorkshopModel> CreateNewWorkshop(WorkshopModel Workshop);
        Task<bool> EmployMember(MemberModel Member);
        Task<WorkshopModel> GetWorkshop(int busId);
        Task<WorkshopModel> GetWorkShop(string regNumber);
        Task<bool> InsertWorkShopService(int workShopRegId, int customServiceId);
    }
}