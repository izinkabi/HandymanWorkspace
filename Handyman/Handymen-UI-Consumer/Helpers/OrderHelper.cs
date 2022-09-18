using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer.Order;
using HandymanUILibrary.API.Consumer.Todo;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Handymen_UI_Consumer.Helpers
{
    public class OrderHelper: PageModel,  IOrderHelper
    {
       
        AuthenticationStateProvider? _authenticationStateProvider;
        ITodoEndPoint _todoEndPoint;
        IServiceEndPoint _serviceEndPoint;
        IOrderEndPoint? _orderEndpoint;
        private List<Service>? serviceDisplayList;
        private List<TodoModel> orderTodoList;
        
  
        private Order? order;
        private List<Order>? ordersDisplayList;
        public SelectList? serviceCategorySelectList { get; set; }
        public List<string>? serviceCategories { get; set; }
        string? ErrorMsg;
        private readonly Handymen_UI_ConsumerContext _context;
        public OrderHelper(Handymen_UI_ConsumerContext contex,
            IOrderEndPoint orderEndPoint, ITodoEndPoint todoEndPoint,IServiceEndPoint serviceEndPoint,
            AuthenticationStateProvider authenticationState)
        {
            _context = contex; 
            _orderEndpoint = orderEndPoint; 
            _serviceEndPoint = serviceEndPoint;
            _authenticationStateProvider = authenticationState;
           _todoEndPoint = todoEndPoint;    
            
        }

        //Get the order's todo-list
        public async Task<List<TodoModel>> GetOrderTodoList(int id)
        {

            orderTodoList = new()!;
            var dotoList = new List<HandymanUILibrary.Models.TodoModel>()!;
            try
            {
                dotoList = await _todoEndPoint.GetTodoListByOrderId(id);
                if(dotoList.Count > 0)
                {
                    foreach (var item in dotoList)
                    {
                        
                            var todoItem = new TodoModel();
                            todoItem.Id = item.Id;
                            todoItem.OrderId = item.OrderId;
                            todoItem.ItemName = item.ItemName;
                            todoItem.Status = item.Status;
                            todoItem.Description = item.Description;
                            todoItem.StartDate = item.StartDate;
                            todoItem.EndDate = item.EndDate;
                            orderTodoList.Add(todoItem);
                        

                    }
                    return orderTodoList;
                }
               
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderTodoList;
        }

        //Get the order the order by its id
        public async Task<Order> GetOrderById(int id)
        {
            if(ordersDisplayList == null)
            {
                await LoadUserOrders();
            }
            foreach(Order o in ordersDisplayList)
            {
                if(o.Id == id)
                {
                    order = o;
                    
                }
            }
            return order;
        }

        //Load all the orders that belong to the given user's id
        public async Task<List<Order>> LoadUserOrders()
        {
            ordersDisplayList = new();
            if(_authenticationStateProvider !=null)
            {
                var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
                var UserId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

                var orders = await _orderEndpoint.GetOrders(UserId);
                await LoadServices();
                //dirty work
                foreach (HandymanUILibrary.Models.OrderModel o in orders)
                {

                    foreach (var service in serviceDisplayList)
                    {
                        if (o.ServiceId == service.Id)
                        {
                            order = new();
                          
                            order.DateCreated = o.DateCreated;
                            order.IsConfirmed = true;
                            order.IsAccepted = o.IsAccepted;
                            order.Id = o.Id;


                            order.ServiceProperty = new();
                            order.ServiceProperty.Name = service.Name;
                            order.ServiceProperty.CategoryName = service.CategoryName;
                            order.ServiceProperty.CategoryDescription = service.CategoryDescription;
                            order.ServiceProperty.Description = service.Description;
                            order.ServiceProperty.ImageUrl = service.ImageUrl;
                            order.IsTracking = false;
                            ordersDisplayList.Add(order);

                        }
                    }

                }
            }
                return ordersDisplayList;
            
        }

        //Loading Services
        public async Task<List<Service>> LoadServices()
        {
            serviceDisplayList = new List<Service>();
            List<HandymanUILibrary.Models.ServiceModel> services = new List<HandymanUILibrary.Models.ServiceModel>();

            try
            {
                //await the endpoint
                services = await _serviceEndPoint.GetServices();

                foreach (var s in services)
                {
                    Service service = new Service();
                    //population
                    service.Name = s.Name;
                    service.Description = s.Description;
                    service.Id = s.Id;
                    //...category
                    service.CategoryId = s.CategoryId;
                    service.CategoryName = s.CategoryName;
                    service.CategoryDescription = s.CategoryDescription;
                    service.CategoryType = s.Type;
                    service.ImageUrl = s.ImageUrl;

                    serviceDisplayList.Add(service);
                    ErrorMsg = null;
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
            return serviceDisplayList;
        }

        //Loading the Service Categories from IServicesEndPoint service
        private async Task LoadServiceCategories()
        {
            try
            {
                List<HandymanUILibrary.Models.ServiceCategoryModel> serviceCategoryModel = new();
                serviceCategories = new List<string>();
                serviceCategoryModel = await _serviceEndPoint.GetServiceCategories();
                foreach (var serviceCategory in serviceCategoryModel)
                {
                    var uiCategory = new ServiceCategory();
                    uiCategory.CategoryName = serviceCategory.CategoryName;
                    uiCategory.CategoryDescription = serviceCategory.CategoryDescription;
                    uiCategory.CategoryId = serviceCategory.CategoryId;
                    if (serviceDisplayList.Count > 0)
                    {
                        uiCategory.Services = serviceDisplayList;
                    }
                    serviceCategories.Add(uiCategory.CategoryName);
                }
                serviceCategorySelectList = new SelectList(serviceCategories);
                ErrorMsg = null;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }

        //Load all orders then select the one you want to delete
        public async Task DeleteOrder(int Id)
        {
           
            if (_orderEndpoint != null)
            {
                try
                {
                   
                  await _orderEndpoint.DeleteOrder(Id);
                     
                }catch(Exception ex)
                {
                    ErrorMsg = ex.Message;
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
