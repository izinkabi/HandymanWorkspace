using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers;

public interface IWorkshopHelper
{

    Task<WorkshopModel> CreateWorkshop(WorkshopModel work);
    Task<bool> AddMemberToWorkShop(MemberModel member);
    Task<WorkshopModel> GetWorkShop(string regNumber);
    Task<WorkshopModel> GetWorkshop();
}