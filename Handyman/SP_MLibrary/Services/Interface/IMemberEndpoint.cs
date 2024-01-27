using SP_MLibrary.Models;

namespace SP_MLibrary.Services.Interface
{
    public interface IMemberEndpoint
    {
        Task<bool> CreateMember(MemberModel member);
        Task<bool> CreateProfile(ProfileModel newProfile);
        Task<ProfileModel> GetProfile(string id);
        Task AddService(MemberModel member);
        Task RemoveService(ServiceModel service, string memberId);

    }
}