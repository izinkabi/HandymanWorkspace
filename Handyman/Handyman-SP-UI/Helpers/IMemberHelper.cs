using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers
{
    public interface IMemberHelper
    {
        Task AddService(ServiceModel service);
        Task<bool> DeleteWorkShopService(int wsServiceId, int ogServiceId);
        Task<MemberModel> GetMember();
        Task<ProfileModel> GetMemberProfile();
        Task<PriceModel> GetServicePrice(int priceId);
        Task<List<CustomServiceModel>> GetWorkShopServices();
        Task<bool> InsertCustomService(ServiceModel service);
        Task<bool> IsServiceProvided(ServiceModel service);
        void RegisterHandyman(MemberModel newHandyman);
        Task<bool> RegisterProfile(ProfileModel profile);
        Task<OrderModel> StampNewOrder(OrderModel newOrder);
        Task<WorkshopModel> StampWorkshopUserAsync(WorkshopModel? work);
        Task<bool> UpdateWorkShopService(CustomServiceModel wsServices);
    }
}