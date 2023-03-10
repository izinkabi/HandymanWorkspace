using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IServiceProviderEndPoint
    {
        Task AddService(ServiceProviderModel provider);
        Task RemoveService(ServiceModel service, string providerId);
        Task CreateServiceProvider(ServiceProviderModel provider);
        Task<ServiceProviderModel> GetProvider(string userId);
        Task<ProfileModel> GetProfile(string id);
        void CreateProfile(ProfileModel newProfile);
    }
}
