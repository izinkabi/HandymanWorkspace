using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class OrderEndpoint : IOrderEndpoint
{

    IAPIHelper? _apiHelper;
    private readonly AuthenticatedUserModel _authedUser;

    public OrderEndpoint(IAPIHelper? aPIHelper, AuthenticatedUserModel authedUser)
    {
        _apiHelper = aPIHelper;
        _authedUser = authedUser;
    }

    //Get the orders relavant to the provider
    //This allows an order filter by the provider's service(s)
    public async Task<IList<OrderModel>> GetNewOrdersByService(int serviceId)
    {
        List<OrderModel>? httpResponse = new();

        try
        {
            if (_authedUser != null && _authedUser.Access_Token != null)
            {
                _apiHelper.InitializeClient(_authedUser.Access_Token);
                httpResponse = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Orders/GetNewOrders?serviceId={serviceId}");
            }


            return httpResponse;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }

    }

    //Get the provider's Order
    //Using the ID of the service provider prompt for Order
    public async Task<List<OrderModel>> GetOrdersByProvider(string? providerId)
    {
        List<OrderModel>? httpResponseMessage = new();

        try
        {
            if (_authedUser != null && _authedUser.Access_Token != null)
            {
                _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedUser.Access_Token}");
                httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Orders/GetProviderOrders?providerId={providerId}");
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
    //an order that was place by a consumer
    public async Task<bool> PostOrder(OrderModel Order)
    {
        string? result = string.Empty;

        try
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (_authedUser != null && _authedUser.Access_Token != null && Order != null)
            {
                _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedUser.Access_Token}");
                httpResponseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Orders/post", Order);
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
    //Change of state/stage, cancellation of completion by the service provider
    public async Task<bool> UpdateTask(TaskModel taskUpdate)
    {
        try
        {
            var result = await _apiHelper.ApiClient.PutAsJsonAsync<TaskModel>("/api/Orders/Update", taskUpdate);
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
            TaskModel taskmodel = await _apiHelper.ApiClient.GetFromJsonAsync<TaskModel>($"/api/Orders/GetTask?Id={id}");
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
            PriceModel price = await _apiHelper.ApiClient.GetFromJsonAsync<PriceModel>($"/api/tasks/gettaskprice?taskId={TaskId}");
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
            var result = await _apiHelper.ApiClient.PostAsJsonAsync($"/api/tasks/posttaskprice?taskId={taskId}&priceId={priceId}", new { });
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
                List<OrderModel> thisMonthOrders = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Orders/GetCurrentMonth?empID={empID}");

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


                List<OrderModel> thisMonthOrders = await _apiHelper.ApiClient.GetFromJsonAsync<List<OrderModel>>($"/api/Orders/GetCurrentWeek?empId={empID}");

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


                IList<OrderModel> thisMonthOrders = await _apiHelper.ApiClient.GetFromJsonAsync<IList<OrderModel>>($"/api/Orders/GetCancelled?empID={empID}");

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

