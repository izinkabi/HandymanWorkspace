using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API.Consumer;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Handymen_UI_Consumer.Areas.Identity.Data;


namespace Handymen_UI_Consumer.Pages
{
    public class OrderDetailsModel : PageModel
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly Handymen_UI_ConsumerContext _context;
        private Order _order;
        private IOrderEndPoint _endPoint;
        private IOrderHelper _orderHelper;
        string? ErrMsg;
        public OrderDetailsModel(Handymen_UI_ConsumerContext context, IHttpContextAccessor httpContextAccessor,
            IOrderEndPoint orderEndPoint, IOrderHelper orderHelper)
        {
            _context = context;
            _orderHelper = orderHelper;
             _endPoint = orderEndPoint;
            _httpContextAccessor = httpContextAccessor;
        }

        //The OrderModel as a class property
        [BindProperty(SupportsGet =true)]
        public Order Order { get { return _order; }  
            set 
            {
                 _order= value;
              
            }
        } 

        //This method displays the Order from the SignalR Hub method
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            List<Order> ordersDisplayList = new List<Order>();

            if (id == null)
            {
                return NotFound();
            }
            try
            {
               var userId = _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                var orders = await _endPoint.GetOrders(userId);

                var serviceDisplayList = await _orderHelper.LoadServices();
                Order order = new();
                foreach (HandymanUILibrary.Models.OrderModel o in orders)
                {

                    foreach (var service in serviceDisplayList)
                    {
                        if (o.ServiceId == service.Id && o.Id == id)
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

                            order.IsAccepted = 1;
                                order.IsConfirmed =false;
                            order.IsTracking = false;
                            _order = order;

                        }
                    }
            }   }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
            
            
         

            //_order = order;
            
            
            //ViewData["order"] = _order;

            return Page();
        }

    }
}
