using Microsoft.AspNetCore.Mvc;
using HandymanUILibrary.API.Consumer;
using HandymanUIApp.Models;
using System.Security.Claims;

namespace HandymanUIApp.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private IOrderEndPoint _orderEndPoint;
        private List<OrderModel>?  AllOrders;
        private IHttpContextAccessor _httpContextAccessor;
        private OrderModel _order;
       

        public OrdersController(ILogger<OrdersController> logger,IOrderEndPoint orderEndPoint,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _orderEndPoint =   orderEndPoint;
            _httpContextAccessor = httpContextAccessor;
        }

        //Main view of the Orders that displays a list of Orders
        public async Task<IActionResult> OrdersHome()
        {

            if(AllOrders==null)
            {
                try
                {
                //httpContextAccessor gets the loggedInUserId
                AllOrders = await LoadOrders(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                }catch(Exception ex)
                {
                    return RedirectToAction("Error",ex.Message);
                }
               
            }
           return View(AllOrders);
        }

        public async Task<IActionResult> Details()
        {

            return View(_order);
        }
        private async Task<List<OrderModel>> LoadOrders(string userId)
        {
            //populate the order list 
            List<HandymanUILibrary.Models.OrderModel> ordersData = new List<HandymanUILibrary.Models.OrderModel>();
            AllOrders = new List<OrderModel>();
            ordersData = await _orderEndPoint.GetOrders(userId);
            foreach(var order in AllOrders)
            {
                var orderModel = new  OrderModel();
                orderModel.Id =  order.Id;
                orderModel.Description = orderModel.Description;
                orderModel.ServiceName = orderModel.ServiceName;
            }
            return AllOrders;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}