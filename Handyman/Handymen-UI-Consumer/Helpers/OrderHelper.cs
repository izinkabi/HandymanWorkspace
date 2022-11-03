using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer.Order.Interface;
using HandymanUILibrary.Models;
using Handymen_UI_Consumer.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Handymen_UI_Consumer.Helpers
{
    public class OrderHelper: PageModel,  IOrderHelper
    {
       
        AuthenticationStateProvider? _authenticationStateProvider;
        IServiceEndPoint _serviceEndPoint;
        IOrderEndPoint? _orderEndpoint;

        List<ServiceModel>? serviceDisplayList;
        List<TaskModel> tasks;
        
  
        OrderModel? order;
        private List<OrderModel>? ordersDisplayList;
        public SelectList? serviceCategorySelectList { get; set; }
        public List<string>? serviceCategories { get; set; }
        string? ErrorMsg;
        private readonly Handymen_UI_ConsumerContext _context;
        string userId;
        public OrderHelper(Handymen_UI_ConsumerContext contex,
            IOrderEndPoint orderEndPoint,IServiceEndPoint serviceEndPoint,
            AuthenticationStateProvider authenticationState)
        {
            _context = contex; 
            _orderEndpoint = orderEndPoint; 
            _serviceEndPoint = serviceEndPoint;
            _authenticationStateProvider = authenticationState;
           
        }

        
        async Task<string> GetUserId()
        {
            try
            {

                var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
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
        public async Task<OrderModel> GetOrderById(int id)
        {
            if(ordersDisplayList == null)
            {
                await LoadUserOrders();
            }
            foreach(OrderModel o in ordersDisplayList)
            {
                if(o.Id == id)
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
                await _orderEndpoint.PostOrder(newOrder);
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
                     
                }catch(Exception ex)
                {
                    ErrorMsg = ex.Message;
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
