using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class RequestData : IRequestData
{
    ISQLDataAccess _dataAccess;

    public RequestData(ISQLDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    //Get all the requests for the given service and their tasks
    //These are actually orders that represent new requests
    public IList<NewRequestModel> GetNewRequests(int serviceId)
    {
        try
        {
            List<NewRequestModel> requests = _dataAccess.LoadData<NewRequestModel, dynamic>("Delivery.spOrderLookUpByService", new { serviceId = serviceId }, "Handyman_DB");
            return requests;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get the tasks of a given order
    public IList<TaskModel> GetTasks(int orderId)
    {
        try
        {
            List<TaskModel> tasks = _dataAccess.LoadData<TaskModel, dynamic>("Delivery.spTaskLookUpByOrder", new { orderId = orderId }, "Handyman_DB");
            return tasks;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get the request(s) of the given provider and their tasks
    public IList<RequestModel> GetRequests(string providerId)
    {
        try
        {
            List<RequestModel> requests = _dataAccess.LoadData<RequestModel, dynamic>("Delivery.spOrderLookUpByService", new { providerId = providerId }, "Handyman_DB");
            return requests;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Post or insert a new request and it's tasks
    //This can only be made by the provider given
    //the fact that it is an order accepted by the provider
    public void InsertRequest(RequestModel request)
    {
        try
        {
            _dataAccess.SaveData("Delivery.spRequestInsert", request, "Handyman_DB");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
