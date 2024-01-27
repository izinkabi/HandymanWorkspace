using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class NegotiationData : INegotiationData
    {
        private readonly ISQLDataAccess _dataAccess;

        public NegotiationData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        //Get the negotiation
        public NegoModel GetNegotiation(int negoId)
        {
            try
            {
                NegoModel negotiation = _dataAccess.LoadData<NegoModel, dynamic>("Request.spNegoLookUp", new { Id = negoId }, "Handyman_DB").First();
                return negotiation;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //Negotiation insert
        public void InsertNego(NegoModel negotiation)
        {
            if (negotiation is null) return;
            try
            {
                _dataAccess.SaveData<NegoModel>("Request.spNegoInsert", negotiation, "Handyman_DB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Negotiation Update
        public void UpdateNego(NegoModel negoUpdate)
        {
            if (negoUpdate is null) return;
            try
            {
                _dataAccess.SaveData("Request.spNegoUpdate",
                    new
                    {
                        Id = negoUpdate.Id,
                        IsCompleted = negoUpdate.IsCompleted,
                        Order_DueDate = negoUpdate.Order_DueDate,
                        LastDateModified = negoUpdate.LastDateModified
                    },
                    "Handyman_DB");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
