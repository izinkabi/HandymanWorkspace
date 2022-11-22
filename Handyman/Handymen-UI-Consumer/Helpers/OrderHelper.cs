using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace Handymen_UI_Consumer.Helpers
{
<<<<<<< HEAD
    public class OrderHelper: IOrderHelper
=======
    public class OrderHelper : PageModel, IOrderHelper
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
    {
        AuthenticationStateProvider? _authenticationStateProvider;
        IOrderEndPoint? _orderEndpoint;
        OrderModel? order;
        List<OrderModel>? ordersDisplayList;


        string? ErrorMsg;
        static string? userId;
        public OrderHelper(IOrderEndPoint orderEndPoint,
            AuthenticationStateProvider authenticationState)
        {
            _orderEndpoint = orderEndPoint;
            _authenticationStateProvider = authenticationState;

        }


        async Task<string> GetUserId()
        {
            try
            {
                if (userId == null)
                {
                    var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                    userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
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
        public async Task<OrderModel> GetOrderById(int id)
        {
            if (ordersDisplayList == null)
            {
                await LoadUserOrders();
            }
<<<<<<< HEAD
                           foreach(OrderModel o in ordersDisplayList)
=======
            foreach (OrderModel o in ordersDisplayList)
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
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
            if (userId == null)
            {
                userId = GetUserId().Result;
            }
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
        public async Task DeleteOrder(int Id)
        {

            if (_orderEndpoint != null)
            {
                try
                {

                    // await _orderEndpoint.DeleteOrder(Id);

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
