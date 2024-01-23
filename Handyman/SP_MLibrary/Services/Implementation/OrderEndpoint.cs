using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace SP_MLibrary.Services.Implementation;

public class OrderEndpoint : IOrderEndpoint
{

    IServiceHelper _ServicesHelper;
    private readonly AuthenticatedUserModel _authedUser;

    public OrderEndpoint(IServiceHelper ServicesHelper, AuthenticatedUserModel authedUser)
    {
        _ServicesHelper = ServicesHelper;
        _authedUser = authedUser;
    }

    //Get the orders relavant to the provider
    //This allows an order filter by the provider's Service(s)
    public async Task<IList<OrderModel>> GetNewOrdersByService(int serviceId)
    {
        List<OrderModel> httpResponse = new();

        try
        {
            // if (_authedUser != null && _authedUser.Access_Token != null)
            //{
            //_ServicesHelper.InitializeClient(_authedUser.Access_Token);
            httpResponse = await _ServicesHelper.ServicesClient.GetFromJsonAsync<List<OrderModel>>($"/api/requests/GetRequests");
            // }


            return httpResponse;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }

    }

    //Get the provider's Order
    //Using the ID of the Service provider prompt for Order
    public async Task<List<OrderModel>> GetOrdersByProvider(string providerId)
    {
        List<OrderModel> httpResponseMessage = new();

        try
        {
            if (_authedUser != null && _authedUser.Access_Token != null)
            {
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Clear();
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Accept.Clear();
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedUser.Access_Token}");
                httpResponseMessage = await _ServicesHelper.ServicesClient.GetFromJsonAsync<List<OrderModel>>($"/Services/Orders/GetProviderOrders?providerId={providerId}");
            }

            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);

        }

    }

    //Post a Order
    //This method is invoked when the provider accepts
    //an order that was placed by a consumer
    public async Task<bool> PostOrder(OrderModel Order)
    {
        string result = string.Empty;

        try
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (_authedUser != null && _authedUser.Access_Token != null && Order != null)
            {
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Clear();
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Accept.Clear();
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _ServicesHelper.ServicesClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedUser.Access_Token}");
                httpResponseMessage = await _ServicesHelper.ServicesClient.PostAsJsonAsync("/Services/Orders/post", Order);
            }
            return httpResponseMessage.IsSuccessStatusCode;


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return false;
        }

    }

    //Update a Order
    //This method is called when there is a change in the Order by the provider
    //Change of state/stage, cancellation of completion by the Service provider
    public async Task<bool> UpdateTask(TaskModel taskUpdate)
    {
        try
        {
            var result = await _ServicesHelper.ServicesClient.PutAsJsonAsync("/Services/Orders/Update", taskUpdate);
            return result.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);

        }

    }

    //Get a task by its ID
    public async Task<TaskModel> GetTask(int id)
    {

        try
        {
            TaskModel taskmodel = await _ServicesHelper.ServicesClient.GetFromJsonAsync<TaskModel>($"/Services/Orders/GetTask?Id={id}");
            return taskmodel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get the price of the given task id
    public async Task<PriceModel> GetTaskPrice(int TaskId)
    {
        if (TaskId == 0) return null;
        try
        {
            PriceModel price = await _ServicesHelper.ServicesClient.GetFromJsonAsync<PriceModel>($"/Services/tasks/gettaskprice?taskId={TaskId}");
            return price;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Insert a new Task price
    public async Task<bool> PostTaskPrice(int taskId, int priceId)
    {
        if (taskId == 0 || priceId == 0) return false;
        try
        {
            var result = await _ServicesHelper.ServicesClient.PostAsJsonAsync($"/Services/tasks/posttaskprice?taskId={taskId}&priceId={priceId}", new { });
            return result.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    //Get Order current month Order
    public async Task<List<OrderModel>> GetCurrentMonthOrders(string empID)
    {
        try
        {
            if (empID != null)
            {
                List<OrderModel> thisMonthOrders = await _ServicesHelper.ServicesClient.GetFromJsonAsync<List<OrderModel>>($"/Services/Orders/GetCurrentMonth?empID={empID}");

                if (thisMonthOrders != null && thisMonthOrders.Count > 0)
                {
                    return thisMonthOrders;
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get this month's Orders
    public async Task<List<OrderModel>> GetCurrentWeekOrders(string empID)
    {
        try
        {
            if (empID != null)
            {


                List<OrderModel> thisMonthOrders = await _ServicesHelper.ServicesClient.GetFromJsonAsync<List<OrderModel>>($"/Services/Orders/GetCurrentWeek?empId={empID}");

                if (thisMonthOrders != null && thisMonthOrders.Count > 0)
                {
                    return thisMonthOrders;
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get Cancelled Orders
    public async Task<IList<OrderModel>> GetCancelledOrders(string empID)
    {
        try
        {
            if (empID != null)
            {


                IList<OrderModel> thisMonthOrders = await _ServicesHelper.ServicesClient.GetFromJsonAsync<IList<OrderModel>>($"/Services/Orders/GetCancelled?empID={empID}");

                if (thisMonthOrders != null && thisMonthOrders.Count > 0)
                {
                    return thisMonthOrders;
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

}

