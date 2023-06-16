using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers;

public interface IBusinessHelper
{
    Task<BusinessModel> GetBusiness();
    Task<BusinessModel> CreateBusiness(BusinessModel newBiz);
    Task<bool> AddMemberToWorkShop(ServiceProviderModel member);
    Task<BusinessModel> GetWorkShop(string regNumber);
}