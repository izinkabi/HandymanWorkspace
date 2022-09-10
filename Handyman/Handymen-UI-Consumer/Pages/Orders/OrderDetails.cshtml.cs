using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Handymen_UI_Consumer.Data;
using Handymen_UI_Consumer.Models;
using HandymanUILibrary.API.Consumer;
using Handymen_UI_Consumer.Helpers;
using Microsoft.AspNetCore.Identity;
using Handymen_UI_Consumer.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Handymen_UI_Consumer.Pages
{
    public class OrderDetailsModel : PageModel
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly Handymen_UI_ConsumerContext _context;
        private Order _order;
        private IOrderEndPoint _endPoint;
        private IOrderHelper _orderHelper;
        string? ErrorMsg;
        public OrderDetailsModel(Handymen_UI_ConsumerContext context, IHttpContextAccessor httpContextAccessor,
            IOrderEndPoint orderEndPoint, IOrderHelper orderHelper)
        {
            _context = context;
            _orderHelper = orderHelper;
            _endPoint = orderEndPoint;
            _httpContextAccessor = httpContextAccessor;
        }

        //The OrderModel as a class property
        [BindProperty(SupportsGet = true)]
        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;

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
                            _order = new();

                            _order.DateCreated = o.DateCreated;
                            _order.IsConfirmed = true;
                            _order.IsAccepted = o.IsAccepted;//this should be a constant but im lazy
                            _order.Id = o.Id;


                            _order.ServiceProperty = new();
                            _order.ServiceProperty.Name = service.Name;
                            _order.ServiceProperty.CategoryName = service.CategoryName;
                            _order.ServiceProperty.CategoryDescription = service.CategoryDescription;
                            _order.ServiceProperty.Description = service.Description;
                            _order.ServiceProperty.ImageUrl = service.ImageUrl;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }


            return Page();
        }

        [Authorize]
        public async Task<RedirectResult> OnPostAsync(int Id)
        {
            try
            {
                await _orderHelper.DeleteOrder(Id);

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;

            }
            return Redirect("/Index");

        }
    }
}
