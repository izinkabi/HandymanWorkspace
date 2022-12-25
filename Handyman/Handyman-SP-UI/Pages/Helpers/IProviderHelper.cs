using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public interface IProviderHelper
    {
        Task AddService(ServiceProviderModel provider);
        Task<ServiceProviderModel>? GetProvider(string userId);
    }
}