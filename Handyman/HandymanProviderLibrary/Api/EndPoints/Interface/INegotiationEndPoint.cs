using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface INegotiationEndPoint
    {
        Task<NegotiationModel> GetNego(int negoId);
        Task<bool> InsertNego(NegotiationModel negotiation);
        Task<bool> UpdateNego(NegotiationModel negotiation);
    }
}