using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.API.Services;
using HandymanUILibrary.Models;
using HandymanUILibrary.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Consumer_SS.Helpers;

public class OrderHelper : IOrderHelper
{

    IOrderEndPoint? _orderEndpoint;
    private readonly AuthenticationStateProvider _authenticationState;
    //private readonly AuthenticatedUserModel _authenticatedUserModel;
    private readonly IServiceEndPoint? _serviceEndPoint;
    private readonly AuthenticatedUserModel _authedUser;
    OrderModel? order;
    List<OrderModel>? ordersDisplayList;


    string? ErrorMsg;
    string? userId;


    public OrderHelper(IOrderEndPoint orderEndPoint,
         AuthenticationStateProvider authenticationState,
        IServiceEndPoint serviceEndPoint, AuthenticatedUserModel authedUser)
    {
        _orderEndpoint = orderEndPoint;
        _authenticationState = authenticationState;
        _serviceEndPoint = serviceEndPoint;
        _authedUser = authedUser;
    }


    //Get the current logged in user's ID
    async Task<string> GetUserId()
    {
        try
        {
            var state = await _authenticationState.GetAuthenticationStateAsync();
            var user = state.User;
            userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
            userId = null;
        }
        return userId;
    }

    //Get the order the order by its id
    public async Task<OrderModel> GetOrderById(int id, string userId)
    {
        if (ordersDisplayList == null)
        {
            await LoadUserOrders();
        }

        foreach (OrderModel o in ordersDisplayList)
        {
            if (o.Id == id)
            {
                order = o;

            }
        }
        return order;
    }

    //Load all the orders that belong to the given user's id
    public async Task<List<OrderModel>> LoadUserOrders()
    {

        ordersDisplayList = (List<OrderModel>?)await _orderEndpoint?.GetOrders();
        if (ordersDisplayList != null)
            foreach (var item in ordersDisplayList)
            {
                item.status = CheckStatus(item);
                if (item.status == 11)
                {
                    cancelledOrders.Add(item);
                }
            }


        return ordersDisplayList;

    }

    //Get orders of the date
    public async Task<List<OrderModel>> GetOrdersOfDate(DateTime date)
    {
        List<OrderModel> orders = await LoadUserOrders();
        List<OrderModel> dateOrders = new();
        if (orders != null)
        {
            foreach (var o in orders)
            {
                if (o.datecreated == date)
                {
                    dateOrders.Add(o);
                }
            }

            if (dateOrders.Count > 0)
            {
                return dateOrders;
            }
        }
        return null;
    }
    //Create an order
    public async Task<int> CreateOrder(OrderModel newOrder)
    {
        try
        {
            if (userId == null)
            {
                await GetUserId();
            }
            newOrder.ConsumerID = userId;
            int orderId = await _orderEndpoint?.PostOrder(newOrder);
            return orderId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    //Lists of stages 
    List<TaskModel>? Accepted;
    List<TaskModel>? inprogressTasks;
    List<TaskModel>? finishedTasks;

    List<OrderModel>? cancelledOrders;

    public List<OrderModel> CancelledOrdersProperty { get { return cancelledOrders; } }

    public async Task<int> NumberOfCancelledRequests()
    {

        try
        {
            await LoadUserOrders();
            if (cancelledOrders != null && cancelledOrders.Count() != 0)
            {
                return cancelledOrders.Count();
            }
            return 0;
        }
        catch (Exception)
        {

            throw;
        }

    }
    //Return the request stage
    internal int CheckStatus(OrderModel order)
    {

        if (order != null)
        {
            //initialize attributes
            Accepted = new()!;
            inprogressTasks = new()!;
            finishedTasks = new()!;
            cancelledOrders = new()!;

            if (order.status <= 3)
            {
                //Check for task status
                foreach (var t in order.Tasks)
                {
                    //STATUS STARTED
                    if (t.tas_status == 1)
                    {
                        Accepted.Add(t);
                    }
                    //STATUS IN PROGRESS
                    if (t.tas_status == 2)
                    {
                        inprogressTasks.Add(t);
                    }
                    //STATUS CLOSED
                    if (t.tas_status == 3)
                    {
                        finishedTasks.Add(t);
                    }

                }


                //Request not started
                if (Accepted.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count == 0)
                {
                    return 1;
                }
                else if (Accepted.Count == 0 && inprogressTasks.Count == 0 && finishedTasks.Count > 0)//Request closed
                {
                    return 3;
                }
                else //Request inprogress
                {
                    return 2;
                }


            }
            else if (order.status == 11)//Request canceled
            {
                return 11;
            }
        }
        return order.status;//Error

    }

    //Load all orders then select the one you want to delete
    public async Task DeleteOrder(OrderModel order)
    {

        if (order != null)
        {
            try
            {
                if (order.ConsumerID == null)
                {
                    order.ConsumerID = await GetUserId();
                }

                await _orderEndpoint.DeleteOrder(order);

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                throw new Exception(ex.Message);
            }

        }
    }

    //Update order status
    public async Task UpdateOrderStatus(OrderModel order) => _orderEndpoint.UpdateOrder(order);

    //Cancel order, order put order stage 11
    public void CancelOrder(OrderModel order)
    {
        if (order != null && order.status != 0 && order.status < 6)
        {
            order.status = 11;//Canceled stage
            UpdateOrderStatus(order);
        }
    }

    //Get the price of the ordered service 
    public async Task<PriceModel> GetOrderPrice(int priceId)
    {
        if (priceId == 0)
        {
            return null;
        }
        try
        {

            PriceModel price = await _serviceEndPoint.GetPrice(priceId);
            return price;
        }
        catch (Exception ex)
        {

            throw;
        }
    }


}
