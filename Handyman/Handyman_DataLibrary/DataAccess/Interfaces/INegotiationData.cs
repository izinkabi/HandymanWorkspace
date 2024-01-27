using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface INegotiationData
    {
        NegoModel GetNegotiation(int negoId);
        void InsertNego(NegoModel negotiation);
        void UpdateNego(NegoModel negoUpdate);
    }
}