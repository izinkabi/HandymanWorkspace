using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API;
using Microsoft.Extensions.Caching.Memory;
using HandymanUILibrary.API.Consumer;
using Microsoft.AspNetCore.Authorization;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Handymen_UI_Consumer.Areas.Identity.Data;

namespace Handymen_UI_Consumer.Pages
{

    public class OrderModel : PageModel
    {
       
        private IOrderHelper _orderHelper;
        private Order order;
        private IServiceEndPoint _serviceEndPoint;
        private string ErrorMsg;
        private List<Service> serviceDisplayList;
        private SignInManager<Handymen_UI_ConsumerUser> SignInManager;


        public DateTime CurrentDateTime { get; }
      
        //Injecting the services 
        public OrderModel(IServiceEndPoint serviceEndPoint, IOrderHelper orderHelper)
        {
            
            _serviceEndPoint = serviceEndPoint;
            _orderHelper = orderHelper;
           
        }

        //public Order Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public Order OrderProperty
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        //Referenced by the component
         List<Service> ServiceDisplayList
            {
                get { return serviceDisplayList; }
            }
        //Get the order for display (the default method on OrderPage redirect or call)
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            //****Caching ****//
            
            //if (!_cache.TryGetValue("order", out Order cacheValue))
            //{
             await LoadOrder(id);

            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(40));

            //    _cache.Set<Order>("order", cacheValue, cacheEntryOptions);
            //}
            //order = cacheValue;
            if (order.Id != id)
            {
                return RedirectToPage("./Orders/OrderDetails");
            }
            

            //*****End Caching****//

            return Page();
        }
        //Confirming the order
       
        //Load / Create the Order 
        private async Task LoadOrder(int? id)
        {

            await LoadServices();

            foreach (var service in serviceDisplayList)
            {
                if (service.Id == id)
                {
                    
                    order = new Order();
                    order.ServiceProperty = new Service();

                    order.ServiceProperty = service;
                    order.DateCreated = DateTime.Now;
                    order.Status = "Active";
                    order.Id = service.Id;
                   
                }
            }
            
        }
        //Load Services from the IServiceEndPoint service
        private async Task LoadServices()
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
        //Cancelling the order 
        private void CancelOrder()
        {
           
        }

        [Authorize]
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            try
            {
                await _orderHelper.DeleteOrder(Id);
            }catch(Exception ex)
            {
                ErrorMsg = ex.Message;
                
            }
           return RedirectToPagePermanent("/Index");
        }
    }
}