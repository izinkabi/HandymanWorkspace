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
                ordersDisplayList = (List<OrderModel>?)await _orderEndpoint?.GetOrders(userId);
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

        //Load all orders then select the one you want to delete
        public async Task DeleteOrder(OrderModel order)
        {

            if (_orderEndpoint != null)
            {
                try
                {
                    order.ConsumerID = await GetUserId();
                    await _orderEndpoint.DeleteOrder(order);

                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
