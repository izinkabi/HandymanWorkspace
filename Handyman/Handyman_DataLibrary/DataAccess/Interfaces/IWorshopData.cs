using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IWorshopData
    {
        void AddProviderServices(Member memeber);
        WorkshopModel CreateBusiness(WorkshopModel workshop);
        void EmployMember(Member member);
        Member GetProvider(string employeeId);
        WorkshopModel GetWorkShop(string regNumber);
        WorkshopModel GetWorkshopById(int? workshopId);
        void InsertWorkShopService(int workShopRegId, int customServiceId);
        void ReleaseEmployee();
    }
}