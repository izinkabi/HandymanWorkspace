using SP_MLibrary.Models;

namespace SP_MLibrary.Services.Interface
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