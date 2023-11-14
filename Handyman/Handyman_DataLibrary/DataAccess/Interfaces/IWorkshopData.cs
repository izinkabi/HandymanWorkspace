using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IWorkshopData
    {
        void AddMemberServices(MemberModel memeber);
        WorkshopModel CreateWorkshop(WorkshopModel workshop);
        void EmployMember(MemberModel member);
        MemberModel GetMember(string employeeId);
        WorkshopModel GetWorkShop(string regNumber);
        WorkshopModel GetWorkshopById(int? workshopId);
        void InsertWorkShopService(int workShopRegId, int customServiceId);
        void ReleaseEmployee();
    }
}