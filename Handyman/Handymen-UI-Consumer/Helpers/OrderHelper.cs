using HandymanUILibrary.API;
using HandymanUILibrary.API.Consumer;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Handymen_UI_Consumer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Handymen_UI_Consumer.Helpers
{
    public class OrderHelper: IOrderHelper
    {
       
        AuthenticationStateProvider? _authenticationStateProvider;
        IServiceEndPoint _serviceEndPoint;
        IOrderEndPoint? _orderEndpoint;
        private List<Service>? serviceDisplayList;
        private Order order;
        private List<Order> ordersDisplayList;
        public SelectList? serviceCategorySelectList { get; set; }
        public List<string> serviceCategories { get; set; }
        string ErrorMsg;
        public OrderHelper(AuthenticationStateProvider authenticationStateProvider,
            IOrderEndPoint orderEndPoint,IServiceEndPoint serviceEndPoint)
        {
            _authenticationStateProvider = authenticationStateProvider; 
            _orderEndpoint = orderEndPoint; 
            _serviceEndPoint = serviceEndPoint; 
        }


        public async Task<List<Order>> LoadUserOrders()
        {
            ordersDisplayList = new();
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var UserId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

            var orders = await _orderEndpoint.GetOrders(UserId);
            await LoadServices();

            foreach (HandymanUILibrary.Models.OrderModel o in orders)
            {
                
                foreach(var service in serviceDisplayList)
                {
                    if (o.ServiceId == service.Id)
                    {
                        order = new();
                        HandymanUILibrary.Models.OrderModel libryOrder = new();

                        libryOrder.ServiceId = service.Id;
                        order.DateCreated = libryOrder.DateCreated;
                        order.IsConfirmed = true;
                        order.IsAccepted = libryOrder.IsAccepted;
                        order.Id = libryOrder.Id;


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
            return ordersDisplayList;
        }

        //Loading Services
        public async Task LoadServices()
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


    }
}
