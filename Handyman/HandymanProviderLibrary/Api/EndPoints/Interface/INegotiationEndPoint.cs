using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface INegotiationEndPoint
    {
        Task<NegoModel> GetNego(int negoId);
        Task<bool> InsertNego(NegoModel negotiation);
        Task<bool> UpdateNego(NegoModel negotiation);
    }
}