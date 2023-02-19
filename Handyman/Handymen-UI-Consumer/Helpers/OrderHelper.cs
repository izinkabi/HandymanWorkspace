using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Handymen_UI_Consumer.Helpers
{
    public class OrderHelper : PageModel, IOrderHelper
    {
        SignInManager<Handymen_UI_ConsumerUser> signInManager;
        IOrderEndPoint? _orderEndpoint;
        OrderModel? order;
        List<OrderModel>? ordersDisplayList;
        AuthenticationStateProvider _authenticationStateProvider;

        string? ErrorMsg;
        string? userId;
        public OrderHelper(IOrderEndPoint orderEndPoint,
            SignInManager<Handymen_UI_ConsumerUser> signInMan, AuthenticationStateProvider authenticationStateProvider)
        {
            _orderEndpoint = orderEndPoint;
            signInManager = signInMan;
            _authenticationStateProvider = authenticationStateProvider;
        }


        //Get the current logged in user's ID
        async Task<string> GetUserId()
        {
            try
            {
                if (userId == null)
                {
                    var user = new ClaimsPrincipal();

                    //(await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                    if (User != null)
                    {
                        if (User.Identity.IsAuthenticated)
                            userId = signInManager.UserManager.GetUserId(User);  //userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

                    }
                    else
                    {
                        user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                        if (user.Identity.IsAuthenticated)
                            userId = signInManager.UserManager.GetUserId(user);
                    }
                }

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
                await LoadUserOrders(userId);
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
        public async Task<List<OrderModel>> LoadUserOrders(string userId)
        {
            //if (userId == null)
            //{
            //    userId = await GetUserId();
            //}
            if (userId != null)
            {
                ordersDisplayList = (List<OrderModel>?)await _orderEndpoint?.GetOrders(userId);
                if (ordersDisplayList != null)
                    foreach (var item in ordersDisplayList)
                    {
                        item.status = CheckStatus(item);
                        if (item.status == 11)
                        {
                            cancelledOrders.Add(item);
                        }
                    }
            }

            return ordersDisplayList;

        }


        //Create an order
        public async Task CreateOrder(OrderModel newOrder)
        {
            try
            {
                if (userId == null)
                {
                    await GetUserId();
                }
                newOrder.ConsumerID = userId;
                await _orderEndpoint?.PostOrder(newOrder);
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
                await LoadUserOrders(userId);
                return cancelledOrders.Count();
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

    }
}
