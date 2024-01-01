using SP_MLibrary.Models;

namespace SP_MLibrary.Services.Interface
{
    public interface INegotiationEndPoint
    {
        Task<NegotiationModel> GetNego(int negoId);
        Task<bool> InsertNego(NegotiationModel negotiation);
        Task<bool> UpdateNego(NegotiationModel negotiation);
    }
}